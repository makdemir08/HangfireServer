namespace ERPPlugin.Dto
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string IdentityNumber { get; set; }
        public DateOnly BirthDate { get; set; }
        public Guid? ParentId { get; set; }
        public Guid TitleId { get; set; }
    }
}
