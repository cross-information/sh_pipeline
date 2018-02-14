<%@ WebHandler Language="C#" Class="pnt" %>

using System;
using System.Web;
using System.Data;
// using System.Data.OracleClient;
using Oracle.DataAccess.Client;

public class pnt : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        // context.Response.Write("Hello World");
        string strConn = "Data Source=127.0.0.1/ORCL;User Id=shpipe;Password=shpipe1234#;";
        using (OracleConnection conn = new OracleConnection(strConn)) {
          conn.Open();
          OracleCommand cmd = conn.CreateCommand();
          cmd.CommandText = "SELECT PNT_ID.NEXTVAL FROM dual";
          decimal nID = (decimal)cmd.ExecuteScalar();
          // context.Response.Write(nID);
          // context.Response.Write(cmd.ExecuteScalar().GetType().ToString());
          // cmd.CommandText = string.Format("INSERT INTO PNT(ID, GEOM) VALUES({0}, \"MDSYS\".\"SDO_GEOMETRY\"(2001,21481, SDO_POINT_TYPE({1},{2},0),null,null))",
		  // cmd.CommandText = string.Format("INSERT INTO PNT(ID, GEOM) VALUES({0}, SDE.ST_PointFromText('POINT ({1}, {2})', 21481)",
		  cmd.CommandText = string.Format("INSERT INTO PNT(OBJECTID, SHAPE) VALUES({0}, SDE.ST_Point({1}, {2}, 2))",
            nID, context.Request["x"], context.Request["y"]);
          // cmd.CommandText = "SELECT COUNT(*) FROM PNT";
          // context.Response.Write(cmd.CommandText);
          cmd.ExecuteNonQuery();
          conn.Close();
        }
        // context.Response.WriteLine(context.Request["x"]);
        // context.Response.WriteLine(context.Request["y"]);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}