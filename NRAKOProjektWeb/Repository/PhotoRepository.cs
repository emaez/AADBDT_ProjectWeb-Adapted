using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using NRAKOProjektWeb.Data;
using NRAKOProjektWeb.Interface.Repository;
using NRAKOProjektWeb.Models;
using NRAKOProjektWeb.Patterns.StorageStrategy;
using NRAKOProjektWeb.Patterns.StorageStrategyFactory;
using NRAKOProjektWeb.ViewModels;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IStorageStrategy _storageStrategy;
        private readonly IHostingEnvironment _hostingEnvironment;
        public PhotoRepository(ApplicationDbContext db, IStorageStrategyFactorySelector storageStrategyFactorySelector,
            IHostingEnvironment environment)
        {
            _db = db;
            _hostingEnvironment = environment;
            _storageStrategy = storageStrategyFactorySelector.GetStorageStrategyFactory(new StorageStrategySelectionData
            {
                StorageStrategyName = _db.ConfigurationEntities.Find("StorageType").Value,
                Params = new Dictionary<string, string> { { "folderPath", Path.Combine(_hostingEnvironment.WebRootPath, "uploads") } }
            }).GetStorageStrategy();
        }
        public List<Photo> GetPhotos()
        {
            return _db.Photos
             .Include(p => p.NRAKOUser)
             .Include(p => p.PhotosHashtags)
             .ThenInclude(ph => ph.Hashtag)
             .OrderByDescending(p => p.DateCreated)
             .Take(10)
             .ToList();
        }

        public IEnumerable<Photo> PhotoDefaultIfEmpty()
        {
            return _db.Photos.DefaultIfEmpty(new Photo { Size = 0, Width = 0, Height = 0 });
        }

        public IEnumerable<Photo> GetphotosByUserID(string userID)
        {
            return _db.Photos
                .Include(p => p.NRAKOUser)
                .Include(p => p.PhotosHashtags)
                .ThenInclude(ph => ph.Hashtag)
                .OrderBy(p => p.DateCreated)
                .Where(p => p.NRAKOUserId == userID);
        }
        public Photo GetphotoByID(int id)
        {
            return _db.Photos
                .Include(p => p.PhotosHashtags)
                .ThenInclude(ph => ph.Hashtag)
                .FirstOrDefault(p => p.Id == id);
        }
        public void RemovePhotoHashTags(int photoID)
        {
            _db.PhotoHashtags.RemoveRange(_db.PhotoHashtags.Where(ph => ph.PhotoId == photoID));
            _db.SaveChanges();
        }

        public void AddHashtagsToPhoto(string[] hashtags, int photoId)
        {
            foreach (var hashtag in hashtags.Distinct())
            {
                Hashtag ht = _db.Hashtags.Where(h => h.Text == hashtag).FirstOrDefault();
                if (ht == null)
                {
                    ht = new Hashtag()
                    {
                        Text = hashtag
                    };
                    _db.Hashtags.Add(ht);
                }

                if (_db.PhotoHashtags.Find(photoId, ht.Id) == null)
                {
                    PhotoHashtag ph = new PhotoHashtag()
                    {
                        HashtagId = ht.Id,
                        PhotoId = photoId
                    };

                    _db.PhotoHashtags.Add(ph);
                }
            }
            _db.SaveChanges();
        }
        public IEnumerable<Photo> FilterData(string sizeRange, string widthRange, string heightRange, string hashtagsFilter, string authorFilter, dynamic viewBag)
        {
            FilterDataViewModel filterData = new FilterDataViewModel();


            if (authorFilter == null)
            {
                authorFilter = "";
            }

            if (hashtagsFilter == null)
            {
                hashtagsFilter = "";
            }


            var sizeRangeParts = sizeRange.Split('-');
            int.TryParse(sizeRangeParts[0], out int sizeRangeMin);
            int.TryParse(sizeRangeParts[1], out int sizeRangeMax);

            filterData.SizeRangeMin = sizeRangeMin;
            filterData.SizeRangeMax = sizeRangeMax;

            viewBag.SizeRangeMin = sizeRangeMin;
            viewBag.SizeRangeMax = sizeRangeMax;

            var widthRangeParts = widthRange.Split('-');
            int.TryParse(widthRangeParts[0], out int widthRangeMin);
            int.TryParse(widthRangeParts[1], out int widthRangeMax);

            filterData.WidthRangeMin = widthRangeMin;
            filterData.WidthRangeMax = widthRangeMax;

            viewBag.WidthRangeMin = widthRangeMin;
            viewBag.WidthRangeMax = widthRangeMax;

            var heightRangeParts = heightRange.Split('-');
            int.TryParse(heightRangeParts[0], out int heightRangeMin);
            int.TryParse(heightRangeParts[1], out int heightRangeMax);

            filterData.HeightRangeMin = heightRangeMin;
            filterData.HeightRangeMax = heightRangeMax;

            filterData.HashtagsFilter = hashtagsFilter;
            filterData.AuthorFilter = authorFilter;

            viewBag.HeightRangeMin = heightRangeMin;
            viewBag.HeightRangeMax = heightRangeMax;

            viewBag.HashtagsFilter = hashtagsFilter;
            viewBag.AuthorFilter = authorFilter;

            viewBag.FilterData = filterData;

            var hashtagsToFilter = filterData.HashtagsFilter.Split(' ').Distinct();


            var hashtagIds = _db.Hashtags.Where(h => hashtagsToFilter.Contains(h.Text)).Select(h => h.Id).ToArray();
            var photoIds = _db.PhotoHashtags.Where(ph => hashtagIds.Contains(ph.HashtagId)).Select(ph => ph.PhotoId).Distinct().ToArray();

            var photos = _db.Photos
                .Include(p => p.NRAKOUser)
                .Include(p => p.PhotosHashtags)
                .ThenInclude(ph => ph.Hashtag)
                .AsQueryable();

            if (hashtagIds.Count() != 0)
            {
                photos = photos.Where(p => photoIds.Contains(p.Id));
            }
            photos = photos.Where(p => p.Size >= (filterData.SizeRangeMin * 1024) && p.Size <= (filterData.SizeRangeMax * 1024))
            .Where(p => p.Width >= filterData.WidthRangeMin && p.Width <= filterData.WidthRangeMax)
            .Where(p => p.Height >= filterData.HeightRangeMin && p.Height <= filterData.HeightRangeMax)
            .Where(p => p.NRAKOUser.UserName.Contains(filterData.AuthorFilter))
            .OrderBy(p => p.DateCreated);

            viewBag.MaxSize = (_db.Photos.Max(p => p.Size) / 1024) + 1;
            viewBag.MaxWidth = _db.Photos.Max(p => p.Width);
            viewBag.MaxHeight = _db.Photos.Max(p => p.Height);

            return photos.ToList();
        }

        public Photo SavePhoto(CreatePhotoViewModel model, string userId)
        {

            string[] hashtags = model.HashtagsString.Split(' ');
            Photo photo = Helpers.Helpers.GetPhotoFromCreatePhotoViewModel(model, userId);
            var extension = model.OriginalImageExtension;


            if (model.DoConversion)
            {
                extension = model.ConversionExtension;
            }

            if (!Helpers.Helpers.GetAvailableFormats().Any(f => f == extension))
            {
                extension = "Jpeg";
            }


            //var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            string uniqueFileName = Helpers.Helpers.GetUniqueFileName(extension);
            //var filePath = Path.Combine(uploads, uniqueFileName);
            long imageSize = 0;
            MemoryStream ms = new MemoryStream();
            using (Image<Rgba32> image = Image.Load<Rgba32>(model.PhotoFile.OpenReadStream()))
            {
                if (model.DoResize)
                {
                    image.Mutate(x => x
                         .Resize(model.ResizeWidth, model.ResizeHeight)
                         );
                }

                image.Save(ms, Helpers.Helpers.ResolveImageEncoder(extension));

                photo.Width = image.Width;
                photo.Height = image.Height;
            }


            photo.Url = _storageStrategy.Store(ms, uniqueFileName, out imageSize);

            ms.Close();
            photo.Size = imageSize;

            _db.Photos.Add(photo);
            AddHashtagsToPhoto(hashtags, photo.Id);
            _db.SaveChanges();

            return photo;
        }
    }
}
