using EscuelaDeCienciasEconomicas.DAL;
using EscuelaDeCienciasEconomicas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EscuelaDeCienciasEconomicas.ActionFilters
{
    public class RBACUser
    {
        public int userId { get; set; }
        public string userNickName { get; set; }
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string userEmail { get; set; }
        public Boolean userIsStaff { get; set; }
        public int userRoleId { get; set; }
        public string userRoleName { get; set; }
        public List<Permission> permissionList = new List<Permission>();

        public RBACUser() { }

        public RBACUser loadSession() {
            if (RaptorAppContext.isSessionActive() && !(RaptorAppContext.getSessionObj(RaptorAppContext.SESSION_USER_OBJ) == null))
            {
                return RaptorAppContext.getSessionObj(RaptorAppContext.SESSION_USER_OBJ) as RBACUser;
            }
            return null;
        }

        public RBACUser(int _userId)
        {
            if (RaptorAppContext.getSessionObj(RaptorAppContext.SESSION_USER_OBJ) ==null)
            {
                this.userId = _userId;
                GetDatabaseUserRolesPermissions();
            }
        }

        private void GetDatabaseUserRolesPermissions()
        {
            //Get user roles and permissions from database...
            try
            {
                RaptorContext db = new RaptorContext();
                RBACUser rbacUser = new RBACUser();
                User user = db.User.Where(c => c.id == this.userId).FirstOrDefault();
                rbacUser.userId = this.userId;
                rbacUser.userNickName = user.username;
                rbacUser.userFirstName = user.first_name;
                rbacUser.userLastName = user.last_name;
                rbacUser.userEmail = user.email;
                rbacUser.userIsStaff = user.is_staff;
                Role role = db.Role.Where(c => c.id == user.role_id).FirstOrDefault();
                rbacUser.userRoleId = role.id;
                rbacUser.userRoleName = role.name;
                rbacUser.permissionList = db.Permission.OrderBy(c => c.Order)
                        .Where(p => db.RolePermission.Any(sp => sp.role_id == role.id && sp.permission_id == p.id))
                        .ToList<Permission>();
                RaptorAppContext.setSessionObj(RaptorAppContext.SESSION_USER_OBJ, rbacUser);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                //ex.StackTrace;
            }
        }

        public bool HasPermission(string requiredControllerPermission, string requiredActionPermission)
        {
            bool bFound = false;
            bFound = (this.permissionList.Where(p => p.Controller == requiredControllerPermission && p.Action == requiredActionPermission).ToList().Count > 0);
            return bFound;
        }
        
    }
}