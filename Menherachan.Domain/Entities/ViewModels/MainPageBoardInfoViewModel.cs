namespace Menherachan.Domain.Entities.ViewModels
{
    public record MainPageBoardInfoViewModel(
        string Prefix,
        int ThreadCount,
        int PostCount,
        int FileCount
    );
}