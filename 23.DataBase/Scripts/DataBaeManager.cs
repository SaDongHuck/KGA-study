using JetBrains.Annotations;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static Cinemachine.DocumentationSortingAttribute;

namespace MyProject
{
    public class DataBaeManager : MonoBehaviour
    {
        public string dpIP = "127.0.0.1";
        public int port = 3306;
        private string dbName = "game";
        private string tableName = "users";
        private string rootPasswd = "1234";
        private MySqlConnection conn; //mysql(mariadb)DB와 연결 상태를 유지하는 객체
        public static DataBaeManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            DBConnect();
        }

        //데이터베이스에 접속
        private async void DBConnect()
        {
            //데이터베이스 접속 설정
            string config = $"server={dpIP};port={port};database={dbName};"+
                $"uid=root;pwd={rootPasswd};charset=utf8;";
            conn = new MySqlConnection(config);
            print($"Mysql 접속 시작. state : {conn.State}");
            await conn.OpenAsync();
            print($"Mysql 접속 성공. state : {conn.State}");
        }

        public async void SignUp(string email, string userName, string passwd)
        {
            StringBuilder pwhash = new StringBuilder(); //비밀번호를 해쉬 키로 변경할 stringBuilder
            using (SHA256 sha256 = SHA256.Create()) //SHA256 해쉬 알고리즘을 사용해 비밀번호를 해쉬키로 변경
            {
                byte[] hashArray = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwd));
                foreach(byte b in hashArray)
                {
                    pwhash.Append($"{b:X2}");
                    //pwhash.Append(b.ToString("X2"));
                }
            }
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText =
                    $"INSERT INTO {tableName} Values ('{email}', '{pwhash}', '{userName}', '초보자',1);";
                int rowsAffected = 0;
                try
                {
                    rowsAffected = await cmd.ExecuteNonQueryAsync();
                }
                finally
                {
                    if (rowsAffected > 0)
                    {
                        //회원가입 완료
                        UIManager.Instance.pageOpen("Popup");
                        UIManager.Instance.popup.PopupOpen("알림", "회원가입에 성공 했습니다",
                            () => { UIManager.Instance.pageOpen("LogIn"); });
                    }
                    else
                    {
                        //회원가입 실패
                        UIManager.Instance.pageOpen("Popup");
                        UIManager.Instance.popup.PopupOpen("알림", "회원가입에 실패했습니다",
                            () => { UIManager.Instance.pageOpen("LogIn"); });
                    }
                }
            }
        }

        public async void Login(string email, string passwd)
        {
            StringBuilder pwhash = new StringBuilder(); //비밀번호를 해쉬 키로 변경할 stringBuilder
            using (SHA256 sha256 = SHA256.Create()) //SHA256 해쉬 알고리즘을 사용해 비밀번호를 해쉬키로 변경
            {
                byte[] hashArray = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwd));
                foreach (byte b in hashArray)
                {
                    pwhash.Append($"{b:X2}");
                    //pwhash.Append(b.ToString("X2"));
                }
            }

            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText =
                    $"SELECT email, username, class, level FROM {tableName} " +
                    $"WHERE email = '{email}' AND pw = '{pwhash}' ;";
                MySqlDataReader reader = await cmd.ExecuteReaderAsync();

                if(reader.Read())
                {
                    //로그인 성공
                    print($"로그인 성공, 이메일:{reader[0]}, 이름 : {reader[1]}, 직업 : {reader[2]}, 레벨 : {reader[3]}");

                    UserData userData = new UserData(
                        reader[0].ToString(), 
                        reader[1].ToString(),
                        reader[2].ToString(), 
                        (int)reader[3]);
                    UIManager.Instance.pageOpen("UserInfo");
                    UIManager.Instance.userInfo.UserInfoOpen(userData);
                }
                else
                {
                    print("로그인 실패");
                    UIManager.Instance.pageOpen("Popup");
                    UIManager.Instance.popup.PopupOpen(
                        "알림",
                        "로그인에 실패 했습니다.",
                        () => UIManager.Instance.pageOpen("LogIn"));
                }

            }
        }

        public async void LevelUP(UserData userData)
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                string config = $"server={dpIP};port={port};database={dbName};" +
                $"uid=root;pwd={rootPasswd};charset=utf8;";
                MySqlConnection conn = new MySqlConnection(config);
                await conn.OpenAsync();

                cmd.Connection = conn;
                cmd.CommandText = $"UPDATE {tableName} SET LEVEL = {userData.level} WHERE email = '{userData.email}';";
                int value = await cmd.ExecuteNonQueryAsync();
                if (value > 0)
                {
                    UIManager.Instance.pageOpen("Popup");
                    UIManager.Instance.popup.PopupOpen("알림", "Level Up.",
                        () => UIManager.Instance.pageOpen("UserInfo"));
                }
                else
                {
                    UIManager.Instance.pageOpen("Popup");
                    UIManager.Instance.popup.PopupOpen("알림", "Error.",
                        () => UIManager.Instance.pageOpen("UserInfo"));
                }
            }
        }

        public async void Delete(UserData userData)
        {
            using(MySqlCommand cmd = new MySqlCommand())
            {
                string config = $"server={dpIP};port={port};database={dbName};" +
                $"uid=root;pwd={rootPasswd};charset=utf8;";
                MySqlConnection conn = new MySqlConnection(config);
                await conn.OpenAsync();

                cmd.Connection = conn;
                cmd.CommandText = $"DELETE FROM {tableName} WHERE email = '{userData.email}';";
                int value = await cmd.ExecuteNonQueryAsync();
                if (value > 0)
                {
                    UIManager.Instance.pageOpen("Popup");
                    UIManager.Instance.popup.PopupOpen("알림", "Delete.",
                        () => UIManager.Instance.pageOpen("UserInfo"));
                }
                else
                {
                    UIManager.Instance.pageOpen("Popup");
                    UIManager.Instance.popup.PopupOpen("알림", "Error.",
                        () => UIManager.Instance.pageOpen("UserInfo"));
                }
            }
        }

        public async Task<List<UserData>> Ranking(int limt10)
        {
            // 데이터베이스 연결 설정
            string config = $"server={dpIP};port={port};database={dbName};" +
                            $"uid=root;pwd={rootPasswd};charset=utf8;";
            List<UserData> rankList = new List<UserData>();

            using (MySqlConnection conn = new MySqlConnection(config))
            {
                try
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;

                        // RANK() 함수 사용으로 순위를 가져옴
                        cmd.CommandText =
                            cmd.CommandText = $"SELECT username, class, level FROM users ORDER BY level DESC LIMIT {limt10};";

                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                // RANK() 결과도 UserData에 포함
                                UserData temp = new UserData(
                                    "",
                                    reader["username"].ToString(),
                                    reader["class"].ToString(),
                                    Convert.ToInt32(reader["level"])
                                );
                                rankList.Add(temp);
                            }
                        }
                    }

                    return rankList; // 랭킹 리스트 반환
                }
                catch (Exception ex)
                {
                    Debug.LogError($"랭킹 조회 중 오류 발생: {ex.Message}");
                    return null; // 오류 발생 시 null 반환
                }
            }
        }

        //데이터 조회가 가능한지 테스트
        public void SelectAll()
        {
            //질의(query)를 수행할 commend 객체를 생성
            MySqlCommand cmd = new MySqlCommand();  

            cmd.Connection = conn;//연결할 db를 입력
            cmd.CommandText =
                $"SELECT * FROM {tableName};";

            //쿼리 결과 데이터셋을 c#에서 사용할 수 있는 형탸로 맞춰줌
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);//쿼리 결과
            DataSet set = new DataSet();

            dataAdapter.Fill(set);

            //데이터가 성공적으로 조회가 되었는지 여부를 DataSet의 테이블 개수와 행 개수를 통해 확인
            bool isSelectSucceed = set.Tables.Count > 0 && set.Tables[0].Rows.Count > 0;

            if (isSelectSucceed)
            {
                print("데이터 조회 성공");
                foreach(DataRow row in set.Tables[0].Rows)
                {
                    print($"이메일 : {row["email"]}, 이름 : {row["username"]}, 직업 : {row["class"]}, 레벨 : {row["level"]}");
                }
            }//조회 성공
            else //조회 실패
            {
                print("데이터 조회 실패");
            }
        }

    }
}
