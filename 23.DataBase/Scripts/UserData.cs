using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace MyProject
{
    public class UserData : MonoBehaviour
    {
        public string email;
        public string userName;
        public string characterClass;
        public int level;

        public UserData(DataRow row) : this(
            row["email"].ToString(), row["username"].ToString(), row["class"].ToString(),
            int.Parse(row["level"].ToString())
            )
        { }
        //다른 생성자를 재사용 하여 생성자 오버로딩

        public UserData(string email, string userName, string characterClass, int level)
        {
            this.email = email;
            this.userName = userName; 
            this.characterClass = characterClass;
            this.level = level; 
        }
    }
}
