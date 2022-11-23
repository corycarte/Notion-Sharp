namespace NotionAPI
{
    using NotionAPI.Databases;
    using NotionAPI.Pages;

    public interface INotion: INotionDatabase, INotionPage { }
}
