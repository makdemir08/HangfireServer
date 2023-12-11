namespace ERPPlugin.Dto
{
    public class Title
    {
        public Guid Id { get; set; }
        public string TitleCode { get; set; }
        public string TitleName { get; set; }
        public string Description { get; set; }
        public Guid ParentId { get; set; }
    }
}
