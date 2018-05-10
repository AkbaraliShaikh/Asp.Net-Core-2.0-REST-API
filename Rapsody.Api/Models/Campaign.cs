using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rapsody.Api.Models
{
    public class Campaign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ClosingDate { get; set; }
        public DateTime LasModifiedDate { get; set; }
        public string OpenedBy { get; set; }
        public string Static { get; set; }
        public string Dynamic { get; set; }
    }
}
