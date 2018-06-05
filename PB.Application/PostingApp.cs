using PB.Domain;
using PB.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace PB.Application
{
    public class PostingApp
    {
        private DB db;

        private void insertPosting(Posting posting)
        {
            var query = string.Format("INSERT INTO b_posting(title, body_text, posting_date, subject_code, views) VALUES('{0}', '{1}', GETDATE(), {2}, 0)", posting.title, posting.body_text, posting.subject_code);
            using (db = new DB())
            {
                db.nonReturnQuery(query);
            }
        }

        private void updatePosting(Posting posting)
        {
            var query = string.Format("UPDATE b_posting SET title = '{0}', body_text = '{1}', posting_date = '{2}', subject_code = {3} WHERE posting_code = {4}", posting.title, posting.body_text, posting.posting_date, posting.subject_code, posting.posting_code);
            using (db = new DB())
            {
                db.nonReturnQuery(query);
            }
        }

        public void chooseQuery(Posting posting)
        {
            if (posting.posting_code > 0)
            {
                this.updatePosting(posting);
            } else
            {
                this.insertPosting(posting);
            }
        }

        public Posting visualization(int posting_code)
        {
            var query = string.Format("exec P_VIEWS {0}", posting_code);
            using (db = new DB())
            {
                var posting = db.queryWithReturn(query);
                return this.listAllInObject(posting).FirstOrDefault();
            }
        }

        public List<int> informations()
        {
            var query = "EXEC P_INFORMATIONS";
            var informations = new List<int>();
            using (db = new DB())
            {
                var datas = db.queryWithReturn(query);
                while (datas.Read())
                {
                    informations.Add(int.Parse(datas["VIEWS"].ToString()));
                    informations.Add(int.Parse(datas["POSTINGS"].ToString()));
                }
                return informations;
            }
        }

        public void deletePosting(int posting_code)
        {
            var query = string.Format("DELETE FROM b_posting WHERE posting_code = {0}", posting_code);
            using (db = new DB())
            {
                db.nonReturnQuery(query);
            }
        }

        private List<Posting> listAllInObject(SqlDataReader allThePostings)
        {
            var postings = new List<Posting>();
            while (allThePostings.Read())
            {
                var postingTemp = new Posting()
                {
                    posting_code = int.Parse(allThePostings["posting_code"].ToString()),
                    title = allThePostings["title"].ToString(),
                    body_text = allThePostings["body_text"].ToString(),
                    posting_date = DateTime.Parse(allThePostings["posting_date"].ToString()),
                    subject_code = int.Parse(allThePostings["subject_code"].ToString()),
                    description_subject = allThePostings["description_subject"].ToString(),
                    views = int.Parse(allThePostings["views"].ToString())
                };

                postings.Add(postingTemp);
            }
            return postings;
        }

        public Posting listByCode(int posting_code)
        {
            var query = string.Format("select p.posting_code as posting_code, p.title as title, p.body_text as body_text, p.posting_date as posting_date, p.subject_code as subject_code, s.description as description_subject, p.views as views from b_posting p inner join b_subject s on p.subject_code = s.subject_code WHERE p.posting_code = {0}", posting_code);
            using (db = new DB())
            {
                var allThePostings = db.queryWithReturn(query);
                return this.listAllInObject(allThePostings).FirstOrDefault();
            }
        }

        public List<Posting> listAll()
        {
            var query = "select p.posting_code as posting_code, p.title as title, p.body_text as body_text, p.posting_date as posting_date, p.subject_code as subject_code, s.description as description_subject, p.views as views from b_posting p inner join b_subject s on p.subject_code = s.subject_code";
            using (db = new DB())
            {
                var allThePostings = db.queryWithReturn(query);
                return this.listAllInObject(allThePostings);
            }
        }

    }
}
