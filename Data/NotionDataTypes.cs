using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotionAPI.Data
{
    public class NotionDataObject
    {
        [JsonProperty("id")]
        public string? Id { get; private set; }

        [JsonProperty("object")]
        public string? ObjectType { get; private set; }

        [JsonProperty("created_time")]
        public DateTime? CreatedTime { get; private set; }
    }

    public partial class NotionPage : NotionDataObject
    {
        [JsonProperty("created_by")]
        public NotionDataObject? CreatedBy { get; set; }

        [JsonProperty("created_time")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("last_edited_time")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("last_edited_by")]
        public NotionDataObject? UpdatedBy { get; set; }

        [JsonProperty("archived")]
        public bool? Archived { get; set; }
    }

    public partial class NotionPageTitle
    {
        [JsonProperty("title")]
        public List<NotionText>? TitleObj { get; set; }
    }

    public partial class NotionText
    {
        [JsonProperty("text")]
        public Text? TitleText { get; set; }

        [JsonProperty("annotations")]
        public Annotations? TitleAnnotations { get; set; }

        [JsonProperty("plain_text")]
        public string? PlainText;

        [JsonProperty("href")]
        public string? Href;

        public partial class Text
        {
            [JsonProperty("content")]
            public string? Content { get; set; }

            [JsonProperty("link")]
            public string? Link { get; set; }
        }

        public partial class Annotations
        {
            [JsonProperty("bold")] public bool? Bold;
            [JsonProperty("italic")] public bool? Italic;
            [JsonProperty("strikethrough")] public bool? Strikethrough;
            [JsonProperty("underline")] public bool? Underline;
            [JsonProperty("code")] public bool? Code;
            [JsonProperty("color")] public string? Color;
        }
    }

    public partial class NotionMultiSelect : NotionDataObject
    {
        [JsonProperty("multi_select")]
        public List<NotionSelect>? MutliSelect { get; set; }
    }

    public partial class NotionSelect : NotionDataObject
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("color")]
        public string? Color { get; set; }
    }

    public partial class NotionDate : NotionDataObject
    {
        [JsonProperty("date")]
        public NotionTimeSpan? Date;

        public partial class NotionTimeSpan
        {
            [JsonProperty("start")]
            public DateTime? Start { get; set; }

            [JsonProperty("end")]
            public DateTime? Stop { get; set; }
        }

        public override string ToString()
        {
            return Date?.Stop == null ? $"{Date?.Start}" : $"{Date?.Start} - {Date?.Stop}";
        }
    }

    public partial class NotionCheckBox : NotionDataObject
    {
        [JsonProperty("checkbox")]
        public bool? Checked;
    }

    public partial class NotionNumber : NotionDataObject
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("number")]
        public float? Number { get; set; }
    }
}
