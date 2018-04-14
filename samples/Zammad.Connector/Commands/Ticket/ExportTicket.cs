using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Zammad.Connector.Commands.Ticket
{
    [XmlRoot("Export")]
    public class ExportTicket
    {
        [XmlArray("Tickets")]
        [XmlArrayItem("Ticket")]
        public List<ExportTicketItem> Tickets { get; set; } = new List<ExportTicketItem>();
    }

    [XmlRoot("Ticket")]
    public class ExportTicketItem
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("GroupId")]
        public int GroupId { get; set; }

        [XmlElement("PriorityId")]
        public int PriorityId { get; set; }

        [XmlElement("StateId")]
        public int StateId { get; set; }

        [XmlElement("OrganizationId")]
        public int? OrganizationId { get; set; }

        [XmlElement("Number")]
        public string Number { get; set; }

        [XmlElement("Title")]
        public string Title { get; set; }

        [XmlElement("OwnerId")]
        public int OwnerId { get; set; }

        [XmlElement("CustomerId")]
        public int CustomerId { get; set; }

        [XmlElement("Note")]
        public string Note { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("TimeUnit")]
        public double? TimeUnit { get; set; }

        [XmlArray("Articles")]
        [XmlArrayItem("Article")]
        public List<ExportTicketArticleItem> Articles { get; set; } = new List<ExportTicketArticleItem>();
    }

    [XmlRoot("Article")]
    public class ExportTicketArticleItem
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Subject")]
        public string Subject { get; set; }

        [XmlElement("ContentType")]
        public string ContentType { get; set; }

        [XmlElement("Body")]
        public string Body { get; set; }

        [XmlElement("Internal")]
        public bool Internal { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }
    }
}
