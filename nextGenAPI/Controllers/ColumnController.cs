using nextGenAPI.DataAccess.Column;
using System.Web.Http;

namespace nextGenAPI.Controllers {

    public class ColumnController : ApiController {

        [HttpGet]
        public IHttpActionResult GetColumns(int tableId) {

            var repo = new ColumnRepository();
            var response = repo.GetColumns(tableId);
            if (response != null) {
                return Ok(response);
            } else {
                return NotFound();
            }
        }
        [HttpPost]
        public IHttpActionResult InsertColumn(ColumnDefinition Column) {

            var repo = new ColumnRepository();
            var response = repo.InsertColumn(Column);
            if (response != null) {
                return Ok(response);
            } else {
                return NotFound();
            }
        }
        [HttpPut]
        public IHttpActionResult UpdateColumn(ColumnDefinition Column) {

            var repo = new ColumnRepository();
            var response = repo.UpdateColumn(Column);
            if (response != null) {
                return Ok(response);
            } else {
                return NotFound();
            }
        }
        //[HttpDelete]
        //public IHttpActionResult DeleteColumn(ColumnDefinition Column) {

        //    var repo = new ColumnRepository();
        //    var response = repo.DeleteColumn(ColumnDefinitionColumn);
        //    if (response != null) {
        //        return Ok(response);
        //    } else {
        //        return NotFound();
        //    }
        //}
    }
}
