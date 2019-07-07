/*
 *┌────────────────────────────────────────────────┐
 *│　描   述：User                                                    
 *│　作   者：fanqi                                              
 *│　版   本：1.0                                              
 *│　创建时间：2019/7/6 周六 20:36:57                        
 *└────────────────────────────────────────────────┘
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coreqi.DbModels
{
    [Table("t_user")]
    public class User
    {
        [KeyAttribute]
        public int id { get; set; }
        [Required, MaxLengthAttribute(20,ErrorMessage ="用户名不能超过20")]
        public string username { get; set; }
        [Required, MaxLengthAttribute(20, ErrorMessage = "密码不能超过20")]
        public string password { get; set; }
        public int? enabled { get; set; }
    }
}
