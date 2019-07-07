/*
 *┌────────────────────────────────────────────────┐
 *│　描   述：UserRepository                                                    
 *│　作   者：fanqi                                              
 *│　版   本：1.0                                              
 *│　创建时间：2019/7/6 周六 20:45:42                        
 *└────────────────────────────────────────────────┘
 */

using Autofac.Annotation;
using Coreqi.Repository.Common.Interface;
using Coreqi.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coreqi.Repository.EfCore.Impl
{
    [Bean(typeof(IUserRepository))]
    public class UserRepository : IUserRepository
    {
        [Autowired]
        public CoreqiDbContext _db { get; set; }
        public void createUser(UserInsertDto user)
        {
            _db.Users.Add(user);
        }
    }
}

////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//            佛祖保佑       永不宕机     永无BUG                  //
////////////////////////////////////////////////////////////////////