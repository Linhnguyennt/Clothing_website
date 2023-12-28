using System.ComponentModel.DataAnnotations;

namespace week05_PTW.Data
{
    public class Order
	{
		public int? Id { get; set; }
		[StringLength(450)]// chiều dài bằng userid trong bảng user dùng cho identity
		public string? Name { get; set; }

		public string? Address { get; set; }

		public string? Email { get; set; }

		public string? PhoneNumber { get; set; }
		public string? UserId { get; set; } = null!;
		public string? Status { get; set; }
		public DateTime CreatedAt { get; set; }

		public ICollection<OrderProduct>? OrderProducts { get; set; }
	}
}
