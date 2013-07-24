using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regulus.Project.TurnBasedRPG.Unity
{
    class UserVerify : Regulus.Game.IStage<WaitVerify>
    {
        private string _Account;
        private string _Password;
        Main _Main;
        public UserVerify(Main main,string account, string password)
        {
            _Main = main;
            // TODO: Complete member initialization
            this._Account = account;
            this._Password = password;
        }

        Action<Regulus.Project.TurnBasedRPG.IVerify> _Verify;
        Regulus.Game.StageLock Regulus.Game.IStage<WaitVerify>.Enter(WaitVerify obj)
        {

            
            obj.StatusMessage = "驗證成功.";
            var notif = _Main.User.VerifyProvider;

            _Verify = (verify) =>
            {
                obj.StatusMessage = "取得驗證元件.";
                var val = verify.Login(_Account, _Password);
                val.OnValue += (success) => 
                {
                    if (success == LoginResult.Success)
                    {
                        obj.StatusMessage = "驗證成功.";
                        
                        obj.ToParking(_Main);
                    }
                    else if (success == LoginResult.RepeatLogin)
                    {
                        
                        obj.StatusMessage = "重複登入請再嘗試.";
                        obj.ToExit(_Main);
                    }
                    else
                    {
                        
                        obj.StatusMessage = "驗證失敗.";
                        obj.ToExit(_Main);
                    }
                };
            };


            if (notif.Ghosts.Length > 0)
            {
                _Verify(notif.Ghosts[0]);
            }
            else
            {
                notif.Supply += _Verify;
            }

            return null;
        }

        void Regulus.Game.IStage<WaitVerify>.Leave(WaitVerify obj)
        {
            var notif = _Main.User.VerifyProvider;
            notif.Supply -= _Verify;
        }

        void Regulus.Game.IStage<WaitVerify>.Update(WaitVerify obj)
        {
            
        }
    }
}
