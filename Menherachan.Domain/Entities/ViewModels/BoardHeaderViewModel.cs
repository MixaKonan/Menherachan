namespace Menherachan.Domain.Entities.ViewModels
{
    public record BoardHeaderViewModel(
        string Prefix,
        string Postfix,
        string Title,
        string Description,
        string Link
    );

}