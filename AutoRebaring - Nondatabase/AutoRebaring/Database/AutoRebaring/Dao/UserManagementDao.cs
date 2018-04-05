using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoRebaring.Database.AutoRebaring.Dao
{
    public enum UserType
    {
        User, Admin, NonAuthorizated
    }
    public class UserManagementDao
    {
        AutoRebaringDbContext db = new AutoRebaringDbContext();
        public UserManagementDao()
        {
        }
        public UserManagement GetUserType(UIApplication uiapp)
        {
            string projectName = uiapp.ActiveUIDocument.Document.ProjectInformation.Name;
            string macAddress = ComputerInfo.GetMacAddress().ToString();

            var res = db.UserManagements.Where(x => x.ProjectName == projectName && x.MacAddress == macAddress);
            if (res.Count() > 0)
            {
                return res.First();
            }
            return null;
        }
        public bool Update(UserManagement user, bool isFirstSetUserName)
        {
            bool isOk = true;
            var res = db.UserManagements.Where(x => x.ProjectName== user.ProjectName && x.WebUsername == user.WebUsername);
            if (res.Count() > 0)
            {
                var data = res.ToList()[0];
                if (data.MacAddress != user.ChangeMacAddress)
                {
                    if (!isFirstSetUserName)
                    {
                        bool isNext = true;
                        if (data.MacAddress != "")
                        {
                            MessageBoxResult mesRes = MessageBox.Show("Tài khoản đang đăng nhập trên một máy khác. Bạn có muốn đăng nhập?", "Cảnh báo", MessageBoxButton.OKCancel);
                            if (mesRes != MessageBoxResult.OK) isNext = false;
                        }
                        if (isNext)
                        {
                            data.ProjectID = user.ProjectID;
                            data.ProjectName = user.ProjectName;
                            data.WebUsername = user.WebUsername;
                            data.WebPassword = user.WebPassword;
                            data.LoginType = user.LoginType;
                            data.ChangeMacAddress = user.ChangeMacAddress;
                        }
                    }
                    isOk = false;
                }
                else
                {
                    data.ProjectID = user.ProjectID;
                    data.ProjectName = user.ProjectName;
                    data.WebUsername = user.WebUsername;
                    data.WebPassword = user.WebPassword;
                    data.LoginType = user.LoginType;
                    data.IsChangePending = false;
                    isOk = true;
                }
            }
            else
            {
                isOk = false;
                if (!isFirstSetUserName)
                {
                    db.UserManagements.Add(user);
                }
            }
            db.SaveChanges();
            if (!isOk)
            {
                if (!isFirstSetUserName)
                {
                    MessageBox.Show("Tài khoản của bạn đang chờ admin xét duyệt. Vui lòng đợi trong giây lát.");
                }
            }
            return isOk;
        }
    }
}
