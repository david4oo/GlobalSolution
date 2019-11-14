using GlobalSolution.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace GlobalSolution.Web.Helpers
{
    public class ComboHelper : IComboHelper
    {

        private readonly DataContext _dataContext;

        public ComboHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }





        public IEnumerable<SelectListItem> GetComboVehicleTypes()
        {

            var list = _dataContext.VehicleTypes.Select(vt => new SelectListItem
            {

                Text = vt.Name,
                Value = $"{vt.Id}"

            })

                .OrderBy(vt => vt.Text)
                .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Select a vehicle type...)",
                Value = "0"

            });
            return list;
        }


















        public IEnumerable<SelectListItem> GetComboCustomers()
        {
            var list = _dataContext.Customers.Select(c => new SelectListItem
            {

                Text = c.User.FullNameWithDocument,
                Value = $"{c.Id}"

            })

                 .OrderBy(c => c.Text)
                 .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Select a customer..)",
                Value = "0"

            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboJobTypes()
        {
            var list = _dataContext.VehicleTypes.Select(j => new SelectListItem
            {

                Text = j.Name,
                Value = $"{j.Id}"

            })

                .OrderBy(j => j.Text)
                .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Select a job type...)",
                Value = "0"

            });
            return list;
        }











        
    }
}

