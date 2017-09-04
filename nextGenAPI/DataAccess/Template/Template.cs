using System;

namespace nextGenAPI.DataAccess.Template {
    public class TemplateDefinition {
        public int TemplateId { get; set; }
        public string TemplateCategory { get; set; }
        public string TemplateName { get; set; }
        public string TemplateNameCategory { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }

        public TemplateDefinition(int templateId, string templateCategory, string templateName, string createdBy, DateTime createdDate, string modifiedBy, DateTime modifiedDate) {
            this.TemplateId = templateId;
            this.TemplateCategory = templateCategory;
            this.TemplateName = templateName;
            this.TemplateNameCategory = string.Format($"{templateCategory.Trim()}-{templateName.Trim()}");
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate.ToShortDateString();
            this.ModifiedBy = modifiedBy;
            this.ModifiedDate = modifiedDate.ToShortDateString();
        }
    }
}