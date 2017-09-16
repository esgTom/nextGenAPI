using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nextGenAPI.DataAccess.Project {
    public class Project {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }

        public Project(int projectId, string projectName, string projectDescription, string createdBy, DateTime createdDate, string modifiedBy, DateTime modifiedDate) {
            this.ProjectId = projectId;
            this.ProjectName = projectName;
            this.ProjectDescription = projectDescription;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate.ToShortDateString();
            this.ModifiedBy = modifiedBy;
            this.ModifiedDate = modifiedDate.ToShortDateString();
        }

    }

}