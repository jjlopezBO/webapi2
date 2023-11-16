    using System;
    namespace cndcAPI.Oracle
{
	    public class Helper
	    {
            private Helper()
		        {
		    }
            private static Helper instance = null;
            public static Helper Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new Helper();
                    }
                    return instance;
                }
            }
            public DateTime formatDate(string s)
            {
                DateTime f;
                try
                {
                    string[] ls = s.Split('.');
                    int d = int.Parse(ls[0]);
                    int m = int.Parse(ls[1]);
                    int a = int.Parse(ls[2]);
                    f = new DateTime(a, m, d);
                }
                catch (Exception)
                {
                    f = DateTime.Now.Date;

                }

                return f;


            }
        }


  
   
    }

