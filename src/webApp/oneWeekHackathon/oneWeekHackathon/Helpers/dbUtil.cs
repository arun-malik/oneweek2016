using System;
using System.Collections.Generic;
using System.Text;

namespace oneWeekHackathon.Helpers
{
    public static class dbUtil
    {
        public static string GenerateQuery(string query, Dictionary<string, object> parameters)
        {
            int count = 0;

            if (parameters != null)
            {
                var queryString = new StringBuilder();
                queryString.Append(query);
                queryString.Append(" ");

                var param = new object[parameters.Count];

                foreach (var pair in parameters)
                {
                    queryString.Append("@" + pair.Key + " = {" + count + "}");

                    param[count] = pair.Value;

                    count++;

                    if (count < parameters.Count)
                        queryString.Append(",");
                }

                query = String.Format(queryString.ToString(), param);
            }

            return query;
        }
    }
}