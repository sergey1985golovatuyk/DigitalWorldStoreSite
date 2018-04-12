using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace DigitalWorldStore.Domain.Entities
{
    [Bind(Exclude = "OrderId")]
    public partial class Order
    {
        [Key]
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [ScaffoldColumn(false)]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Введите Ваше Имя")]
        [DisplayName("Имя:")]
        [StringLength(160)]
        public string CustomerFirstName { get; set; }

        [Required(ErrorMessage = "Введите Вашу Фамилию")]
        [DisplayName("Фамилия:")]
        [StringLength(160)]
        public string CustomerLastName { get; set; }

        [DisplayName("Отчество:")]
        public string CustomerMiddleName { get; set; }

        [Required(ErrorMessage = "Введите адрес доставки")]
        [StringLength(70)]
        [DisplayName("Адрес Доставки:")]
        public string DeliveryAddress { get; set; }

        [Required(ErrorMessage = "Введите Ваш номер телефона")]
        [StringLength(24)]
        [DisplayName("Номер телефона:")]
        public string CustomerPhone { get; set; }

        [Required(ErrorMessage = "Введите адрес Вашей электронной почты")]
        [DisplayName("Адрес электронной почты:")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
    ErrorMessage = "Такой адрес не существует.")]
        public string CustomerEmail { get; set; }

        [ScaffoldColumn(false)]
        public decimal TotalPrice { get; set; }
        [ScaffoldColumn(false)]
        public DateTime Created { get; set; }
    }
}
