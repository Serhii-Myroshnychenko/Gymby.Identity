using System.Threading.Tasks;

namespace Gymby.Identity.Interfaces
{
    public interface IFileService
    {
        Task CreateFoldersInContainer(string containerName, string userId);
    }
}
