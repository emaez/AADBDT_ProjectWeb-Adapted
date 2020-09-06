using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.EntityFrameworkCore;
using NRAKOProjektWeb.Data;
using NRAKOProjektWeb.Models;
using NRAKOProjektWeb.Patterns.MutationFactory;
using NRAKOProjektWeb.Patterns.StorageStrategy;
using NRAKOProjektWeb.ViewModels;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace NRAKOProjektWeb.Helpers
{
    public static class Helpers
    {

        public static IEnumerable<PhotoViewModel> GetPhotoViewModelsFromPhotos(IEnumerable<Photo> photos, string userId, bool isAdmin)
        {
            List<PhotoViewModel> pvms = new List<PhotoViewModel>();

            foreach (var photo in photos)
            {
                pvms.Add(new PhotoViewModel()
                {
                    Id = photo.Id,
                    Author = photo?.NRAKOUser?.UserName,
                    Description = photo?.Description,
                    Hashtags = photo?.PhotosHashtags?.Select(ph => new string(ph.Hashtag.Text)).ToList(),
                    UploadedAt = photo.DateCreated,
                    Url = photo.Url,
                    Size = photo.Size,
                    Height = photo.Height,
                    Editable = isAdmin ? true : photo.NRAKOUserId == userId ? true : false
                });
            }

            return pvms;
        }

        public static Photo GetPhotoFromPhotoViewModel(PhotoViewModel pvm, string userId)
        {
            Photo photo = new Photo();
            DateTime now = DateTime.Now;
            photo.DateCreated = now;
            photo.DateModified = now;
            photo.Description = pvm.Description;
            photo.NRAKOUserId = userId;

            return photo;

        }

        public static EditPhotoViewModel GetEditPhotoViewModelFromPhoto(Photo photo)
        {
            EditPhotoViewModel epvm = new EditPhotoViewModel();
            epvm.Description = photo.Description;
            epvm.Hashtags = String.Join(' ', photo.PhotosHashtags.Select(ph => new string(ph.Hashtag.Text)).ToList());
            epvm.PhotoId = photo.Id;
            epvm.Url = photo.Url;

            return epvm;
        }

        public static string GetUniqueFileName(string extension)
        {
            return Guid.NewGuid().ToString() + "." + extension;
        }

        internal static Photo GetPhotoFromCreatePhotoViewModel(CreatePhotoViewModel model, string userId)
        {
            Photo photo = new Photo();
            DateTime now = DateTime.Now;
            photo.DateCreated = now;
            photo.DateModified = now;
            photo.Description = model.Description;
            photo.NRAKOUserId = userId;

            return photo;
        }

        internal static void SetFilterBounds(IEnumerable<Photo> photosEnumerable, dynamic viewBag)
        {
            var sizeMax = (photosEnumerable.Max(p => p.Size) / 1024) + 1;
            var widthMax = photosEnumerable.Max(p => p.Width);
            var heightMax = photosEnumerable.Max(p => p.Height);

            viewBag.SizeRangeMin = 0;
            viewBag.SizeRangeMax = sizeMax;

            viewBag.WidthRangeMin = 0;
            viewBag.WidthRangeMax = widthMax;

            viewBag.HeightRangeMin = 0;
            viewBag.HeightRangeMax = heightMax;

            viewBag.MaxSize = sizeMax;
            viewBag.MaxWidth = widthMax;
            viewBag.MaxHeight = heightMax;
        }

        internal static Photo SavePhoto(CreatePhotoViewModel model, IStorageStrategy storageStrategy, string userId, ApplicationDbContext db)
        {

            string[] hashtags = model.HashtagsString.Split(' ');
            Photo photo = GetPhotoFromCreatePhotoViewModel(model, userId);
            var extension = model.OriginalImageExtension;


            if (model.DoConversion)
            {
                extension = model.ConversionExtension;
            }

            if (!GetAvailableFormats().Any(f => f == extension))
            {
                extension = "Jpeg";
            }


            //var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            string uniqueFileName = GetUniqueFileName(extension);
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

                image.Save(ms, ResolveImageEncoder(extension));

                photo.Width = image.Width;
                photo.Height = image.Height;
            }


            photo.Url = storageStrategy.Store(ms, uniqueFileName, out imageSize);

            ms.Close();
            photo.Size = imageSize;

            db.Photos.Add(photo);
            AddHashtagsToPhoto(db, hashtags, photo.Id);
            db.SaveChanges();

            return photo;
        }

        public static IImageEncoder ResolveImageEncoder(string extension)
        {

            extension = extension.First().ToString().ToUpper() + extension.Substring(1);

            if (extension == "Jpg")
            {
                extension = "Jpeg";
            }
            Assembly assembly = typeof(JpegEncoder).Assembly;
            string encoderName = "SixLabors.ImageSharp.Formats." + extension + "." + extension + "Encoder";
            Type encoderType = assembly.GetType(encoderName);
            return (IImageEncoder)Activator.CreateInstance(encoderType);


        }

        internal static IEnumerable<string> GetAvailableActions()
        {
            return new List<string>() { "Sepia", "Blur", "Vignete", "Resize" };
        }

        internal static IEnumerable<string> GetAvailableFormats()
        {
            return new List<string>() { "Jpeg", "Png", "Bmp", "Gif" };
        }

        internal static IEnumerable<Photo> FilterData(string sizeRange, string widthRange, string heightRange, string hashtagsFilter, string authorFilter, dynamic viewBag, ApplicationDbContext db)
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


            var hashtagIds = db.Hashtags.Where(h => hashtagsToFilter.Contains(h.Text)).Select(h => h.Id).ToArray();
            var photoIds = db.PhotoHashtags.Where(ph => hashtagIds.Contains(ph.HashtagId)).Select(ph => ph.PhotoId).Distinct().ToArray();

            var photos = db.Photos
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

            viewBag.MaxSize = (db.Photos.Max(p => p.Size) / 1024) + 1;
            viewBag.MaxWidth = db.Photos.Max(p => p.Width);
            viewBag.MaxHeight = db.Photos.Max(p => p.Height);

            return photos.ToList();
        }

        internal static DownloadPhotoViewModel GetDownloadPhotoViewModelFromPhoto(Photo photo)
        {
            DownloadPhotoViewModel dpvm = new DownloadPhotoViewModel();
            dpvm.AvailableActions = GetAvailableActions();
            dpvm.AvailableFormats = GetAvailableFormats();
            dpvm.ResizeWidth = photo.Width;
            dpvm.ResizeHeight = photo.Height;
            dpvm.PhotoUrl = photo.Url;
            dpvm.OriginalImageExtension = Path.GetExtension(photo.Url).Substring(1);

            return dpvm;
        }

        internal static void AddHashtagsToPhoto(ApplicationDbContext db, string[] hashtags, int photoId)
        {
            foreach (var hashtag in hashtags.Distinct())
            {
                Hashtag ht = db.Hashtags.Where(h => h.Text == hashtag).FirstOrDefault();
                if (ht == null)
                {
                    ht = new Hashtag()
                    {
                        Text = hashtag
                    };
                    db.Hashtags.Add(ht);
                }

                if (db.PhotoHashtags.Find(photoId, ht.Id) == null)
                {
                    PhotoHashtag ph = new PhotoHashtag()
                    {
                        HashtagId = ht.Id,
                        PhotoId = photoId
                    };

                    db.PhotoHashtags.Add(ph);
                }
            }
        }

        internal static byte[] PerformMutations(string photoUrl, string extension, IEnumerable<MutationAction> actions, Microsoft.AspNetCore.Http.HttpRequest request, IMutationActionFactorySelector mutationActionFactorySelector)
        {
            byte[] fileBytes;
            using (WebClient webClient = new WebClient())
            {

                if (photoUrl.StartsWith("~"))
                {
                    photoUrl = $"{request.Scheme}://{request.Host}{photoUrl.TrimStart('~')}";
                }

                byte[] data = webClient.DownloadData(photoUrl);

                using (MemoryStream mem = new MemoryStream(data))
                {

                    MemoryStream ms = new MemoryStream();
                    using (Image<Rgba32> image = Image.Load<Rgba32>(mem))
                    {

                        foreach (var action in actions)
                        {
                            mutationActionFactorySelector.GetMutationActionFactory(action).GetMutationAction().Mutate(image);
                        }

                        image.Save(ms, ResolveImageEncoder(extension));

                    }
                    fileBytes = ms.ToArray();
                    ms.Close();

                }

            }
            return fileBytes;
        }
    }
}

