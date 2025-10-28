namespace Domain.Models
{
    public class Attachment
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public DateTime UploadedDate { get; set; }
        public int UploadUserId { get; set; }
        public int AttachmentStatus { get; set; }

        public string EntityName { get; set; } // e.g., "LeaveApplication", "JobPost"
        public int EntityId { get; set; }
    }
}
