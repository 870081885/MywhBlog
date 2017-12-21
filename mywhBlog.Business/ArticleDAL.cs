using mywhBlog.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mywhBlog.Business
{
    public class ArticleDAL
    {
        public DataSet getLatelyArticleList(int page)
        {
            DataSet ds = new DataSet();

            int count = 10;

            string strSql = @"select * from (SELECT ROW_NUMBER() OVER (order by a.createTime desc)AS rowNumber, title,introduction,img,convert(varchar(100),createTime,23) createTime,categoryName,IsNULL(b.browserNum,0) browserNum,
                              a.id articleId,a.categoryId,m.menuKey  
                              from tb_article a 
                              left JOIN tb_category c on a.categoryId=c.categoryId 
                              inner join tb_blogmenu m on a.categoryId=m.categoryId 
                              left join (select articleId,count(*) browserNum from browserecord group by articleId)b  on a.id=b.articleId
                              where a.status=1)tt where rowNumber>= " + (page - 1) * count + " and rowNumber<=" + page * count;
            DataTable dt = Utility.SqlHelper.GetDataTable(strSql);
            ds.Tables.Add(dt.Copy());
            ds.Tables[0].TableName = "dt0";
            strSql = "select(count(*) - 1)/"+ count + " + 1 from tb_article where status= 1";
            dt = Utility.SqlHelper.GetDataTable(strSql);
            ds.Tables.Add(dt.Copy());
            return ds;
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="menuKey">菜单键</param>
        /// <param name="listCount">列表条数</param>
        /// <param name="page">页数</param>
        /// <returns></returns>
        public MyResult getArticleList(string menuKey, int listCount,int page)
        {
            MyResult result = new MyResult();
            result.flag = 1;
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@menuKey",SqlDbType.VarChar){ Value = menuKey }
            };
            
            string strSql = "select *,menuName title,menuName keywords,menuName description from tb_blogmenu where menuKey=@menuKey";
            DataTable dtMenuInfo = Utility.SqlHelper.GetDataTable(strSql, param);
            result.obj = dtMenuInfo;

            strSql = @"select * from (SELECT ROW_NUMBER() OVER (order by createtime desc)AS rowNumber, a.id articleId,title,introduction,convert(varchar(100),createTime,23) createTime,categoryName,b.browserNum
                     from tb_article a
                    INNER JOIN tb_category c on a.categoryId = c.categoryId
                    left join(select articleId, count(*) browserNum from browserecord group by articleId)b on a.id = b.articleId
                    where a.status= 1 and a.categoryId in (select categoryId from tb_category where categoryId = '" + dtMenuInfo.Rows[0]["categoryId"] + "' or parentId = '" + dtMenuInfo.Rows[0]["categoryId"] + @"') 
                    )tt where rowNumber>=" + (System.Convert.ToInt32(page) - 1) * listCount + " and rowNumber<=" + System.Convert.ToInt32(page) * listCount;
            DataTable dt = Utility.SqlHelper.GetDataTable(strSql);
            result.obj2 = dt;

            strSql = "select (count(*)-1)/" + listCount + " +1 from tb_article where status=1 and categoryId in" +
                       "(select categoryId from tb_category where categoryId='" + dtMenuInfo.Rows[0]["categoryId"] + "' or parentId='" + dtMenuInfo.Rows[0]["categoryId"] + "')";
            result.obj3 = Utility.SqlHelper.ExecuteScalar(strSql);

            return result;
        }

        public MyResult getArticleContent(string menuKey, int articleId)
        {
            MyResult result = new MyResult();
            result.flag = 1;
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@articleId",SqlDbType.Int){ Value = articleId }
            };
            //文章内容
            string strSql = @"select a.*,a.id articleId,s.nikeName, IsNULL(b.browserNum,0) browserNum,c.categoryName 
                                from tb_article a 
                                inner join tb_category c on a.categoryId=c.categoryId 
                                inner join system_users s on a.userId=s.id 
                                left join (select articleId,count(*) browserNum from browserecord group by articleId)b  on a.id=b.articleId
                                where a.id=@articleId";
            DataTable dt = Utility.SqlHelper.GetDataTable(strSql, param);
            result.obj = dt;

            //标签
            strSql = "select * from tb_article_tag where articleId=@articleId";
            dt = Utility.SqlHelper.GetDataTable(strSql, param);
            result.obj2 = dt;

            //上下一篇
            strSql = @"select * from  (select top 1 1 type,id articleId,title from tb_article where id<@articleId and status=1)t1
                           UNION all
                           select * from  (select top 2  2 type,id articleId,title from tb_article where id>@articleId and status=1)t2";
            dt = Utility.SqlHelper.GetDataTable(strSql, param);
            result.obj3 = dt;

            return result;
        }

        public DataTable getHotTagList()
        {
            string strSql = @"select top 10 count(*),tagName 
                            from tb_article_tag t inner join browserecord b 
                            on t.articleId=b.articleId 
                            group by tagName
                            order by count(*) desc ";
            DataTable dt = Utility.SqlHelper.GetDataTable(strSql);
            return dt;
        }


        // 访问文章
        public void clickArticle(string articleId,string ip)
        {
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@articleId",SqlDbType.Int){ Value = articleId },
                new SqlParameter("@ip",SqlDbType.VarChar){ Value = ip }
            };
            string strSql = @"insert into browseRecord(date,ip,articleId) 
                              select getdate(),@ip,@articleId 
                              where not EXISTS(select * from browseRecord where ip=@ip and date=convert(varchar(100),getdate(),23) and articleId=@articleId)";
             Utility.SqlHelper.ExecuteNonQuery(strSql, param);
        }
    }
}
