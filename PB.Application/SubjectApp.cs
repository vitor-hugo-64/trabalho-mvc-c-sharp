using PB.Domain;
using PB.Repository;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace PB.Application
{
    public class SubjectApp
    {
        private DB db;

        private void insertSubject(Subject subject)
        {
            var query = string.Format("INSERT INTO b_subject VALUES('{0}')", subject.description);
            using (db = new DB())
            {
                db.nonReturnQuery(query);
            }
        }

        private void updateSubject(Subject subject)
        {
            var query = string.Format("UPDATE b_subject SET description = '{0}' WHERE subject_code = {1}", subject.description, subject.subject_code);
            using (db = new DB())
            {
                db.nonReturnQuery(query);
            }
        }

        public void chooseQuery(Subject subject)
        {
            if (subject.subject_code > 0)
            {
                this.updateSubject(subject);
            } else
            {
                this.insertSubject(subject);
            }
        }

        public void deleteSubject(int subject_code)
        {
            var query = string.Format("exec P_DELETE_SUBJECT {0}", subject_code);
            using (db = new DB())
            {
                db.nonReturnQuery(query);
            }
        }

        public Subject listByCode(int code)
        {
            var query = string.Format("SELECT subject_code, description FROM b_subject WHERE subject_code = {0}", code);
            using (db = new DB())
            {
                var allTheSubjects = db.queryWithReturn(query);
                return this.listAllInObject(allTheSubjects).FirstOrDefault();
            }
        }

        public List<Subject> listAll()
        {
            var query = "SELECT subject_code, description FROM b_subject";
            using (db = new DB())
            {
                var allTheSubjects = db.queryWithReturn(query);
                return this.listAllInObject(allTheSubjects);
            }
        }

        private List<Subject> listAllInObject(SqlDataReader allTheSubjects)
        {
            var subjects = new List<Subject>();
            while (allTheSubjects.Read())
            {
                var subjectTemp = new Subject()
                {
                    subject_code = int.Parse(allTheSubjects["subject_code"].ToString()),
                    description = allTheSubjects["description"].ToString()
                };

                subjects.Add(subjectTemp);
            }
            return subjects;
        }
        
    }
}
