using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
// using System.Data.OracleClient;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using Newtonsoft.Json.Linq;

/// <summary>
/// Summary description for ListHandler
/// </summary>
public class ListHandler : IHttpHandler
{
	public ListHandler()
	{
		//
		// TODO: Add constructor logic here
		//
	}

	public void ProcessRequest(HttpContext context)
	{
		context.Response.ContentType = "text/plain";
		// context.Response.Write("Hello World");
		JArray reponse = new JArray();
		string strConn = "Data Source=127.0.0.1/ORCL;User Id=shpipe;Password=shpipe1234#;";
		using (OracleConnection conn = new OracleConnection(strConn)) {
			conn.Open();
			OracleCommand cmd = conn.CreateCommand();
			// cmd.CommandText = "SELECT ID,MDSYS.ST_GEOMETRY(GEOM).Get_WKT() FROM PNT ORDER BY ID";
			cmd.CommandText = "SELECT OBJECTID,SDE.ST_AsText(SHAPE) FROM PNT ORDER BY OBJECTID"; 
			OracleDataReader reader = cmd.ExecuteReader();
			while (reader.Read()) {
				// context.Response.Write(reader[1]);
				// context.Response.Write("\n");
				string s = (string)reader[1];
				s = s.Substring(9);
				s = s.Substring(0, s.Length - 1);
				string[] ss = s.Split(new char[] { ' '});
				JArray pt = new JArray();
				pt.Add(decimal.Parse(ss[0].Trim()));
				pt.Add(decimal.Parse(ss[1].Trim()));
				reponse.Add(pt); //*/
			}
			reader.Close();
			// decimal nID = (decimal)cmd.ExecuteScalar();
			// context.Response.Write(nID);
			// context.Response.Write(cmd.ExecuteScalar().GetType().ToString());
			// cmd.CommandText = string.Format("INSERT INTO PNT(ID, GEOM) VALUES({0}, \"MDSYS\".\"SDO_GEOMETRY\"(2001,4326, SDO_POINT_TYPE({1},{2},0),null,null))",
			// nID, context.Request["x"], context.Request["y"]);
			// cmd.CommandText = "SELECT COUNT(*) FROM PNT";
			// context.Response.Write(cmd.ExecuteScalar());
			// cmd.ExecuteNonQuery();
			conn.Close();
		}
		// context.Response.WriteLine(context.Request["x"]);
		// context.Response.WriteLine(context.Request["y"]);
		context.Response.Write(reponse.ToString());
	}

	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
}