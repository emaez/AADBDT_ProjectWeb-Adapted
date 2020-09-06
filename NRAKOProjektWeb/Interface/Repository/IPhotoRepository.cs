using NRAKOProjektWeb.Models;
using NRAKOProjektWeb.Patterns.StorageStrategy;
using NRAKOProjektWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Interface.Repository
{
    public interface IPhotoRepository
    {
        List<Photo> GetPhotos();
        IEnumerable<Photo> PhotoDefaultIfEmpty();
        IEnumerable<Photo> GetphotosByUserID(string userID);
        Photo GetphotoByID(int id);
        void RemovePhotoHashTags(int photoID);
        void AddHashtagsToPhoto(string[] hashtags, int photoId);
        IEnumerable<Photo> FilterData(string sizeRange, string widthRange, string heightRange, string hashtagsFilter, string authorFilter, dynamic viewBag);
        Photo SavePhoto(CreatePhotoViewModel model, string userId);
    }
}
