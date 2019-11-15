using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GlobalSolution.Web.Helpers
{
    public interface IComboHelper
    {
        IEnumerable<SelectListItem> GetComboVehicleTypes();
        IEnumerable<SelectListItem> GetComboCustomers();
        IEnumerable<SelectListItem> GetComboJobTypes();
        IEnumerable<SelectListItem> GetComboRoles();

    }
}
