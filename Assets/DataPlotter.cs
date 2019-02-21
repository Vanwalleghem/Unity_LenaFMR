using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlotter : MonoBehaviour {
    /*
    public Color JetColorMap(double v, double vmin, double vmax)
    {
        Color color_map = Color.white;
        double dv;
        if (v < vmin)
            v = vmin;
        if (v > vmax)
            v = vmax;
        dv = vmax - vmin;

        if (v < (vmin + 0.25 * dv))
        {
            color_map.r = 0;
            color_map.g = System.Convert.ToSingle(4 * (v - vmin) / dv);
        }
        else if (v < (vmin + 0.5 * dv))
        {
            color_map.r = 0;
            color_map.b = System.Convert.ToSingle(1 + 4 * (vmin + 0.25 * dv - v) / dv);
        }
        else if (v < (vmin + 0.75 * dv))
        {
            color_map.r = System.Convert.ToSingle(4 * (v - vmin - 0.5 * dv) / dv);
            color_map.b = 0;
        }
        else
        {
            color_map.g = System.Convert.ToSingle(1 + 4 * (vmin + 0.75 * dv - v) / dv);
            color_map.b = 0;
        }
        return color_map;
    }
    */
    // Name of the input file, no extension
    public string inputfile;

    // List for holding data from CSV reader
	private List<Dictionary<string, object>> pointList;
    private List<Dictionary<string, object>> cividis_colormap;

    // Indices for columns to be assigned
    public int columnX = 0;
	public int columnY = 1;
	public int columnZ = 2;
	public int columnC = 3;
    public int columnR = 4;

    // Full column names
    public string xName;
	public string yName;
	public string zName;
	public string cName;
    public string rName;
    public string R_col;
    public string G_col;
    public string B_col;

    // The prefab for the data points to be instantiated
    public GameObject Colored_sphere;	

	//Will hold the instantiated prefabs
	public GameObject PointHolder;

	// Use this for initialization
	void Start () {

		// Set pointlist to results of function Reader with argument inputfile
		pointList = CSVReader.Read(inputfile);
        //cividis_colormap = CSVReader.Read("cividis");

        //Log to console
        Debug.Log(pointList);
        //Debug.Log(cividis_colormap);
        // Declare list of strings, fill with keys (column names)
        List<string> columnList = new List<string>(pointList[1].Keys);
        //List<string> cividis_colormap_columns = new List<string>(cividis_colormap[1].Keys);

        // Print number of keys (using .count)
        //Debug.Log("There are " + columnList.Count + " columns in CSV");

        //foreach (string key in columnList)
        //Debug.Log("Column name is " + key);
        // Assign column name from columnList to Name variables
        xName = columnList[columnX];
		yName = columnList[columnY];
		zName = columnList[columnZ];
		cName = columnList[columnC];
        rName = columnList[columnR];

        //R_col = cividis_colormap_columns[0];
        //G_col = cividis_colormap_columns[1];
        //B_col = cividis_colormap_columns[2];
        //instantiate prefab
        //Instantiate(Colored_sphere, new Vector3(0,0,0), Quaternion.identity);
        //Instantiate(MagentaSphere, new Vector3(0,0,0), Quaternion.identity);
        //Instantiate(CyanSphere, new Vector3(0,0,0), Quaternion.identity);
        //Loop through Pointlist
        Color objColor;
        objColor = Colored_sphere.GetComponent<Renderer>().sharedMaterial.color;
        /*
        float[,] colormap = new float[100,3];
        int percent_1 = 40; int percent_2 = 80;
        for (var i=0; i < 100; i++)
        {
            //print(i);
            colormap[i, 2] = Mathf.Clamp(System.Convert.ToSingle(i) / percent_1, 0, 1);
            if (i >= percent_1)
            {
                colormap[i, 0] = Mathf.Clamp((System.Convert.ToSingle(i) - percent_1) / (percent_2- percent_1), 0, 1);
            }
            if (i >= percent_2)
            {
                colormap[i, 1] = Mathf.Clamp((System.Convert.ToSingle(i) - percent_2) / (100- percent_2), 0, 1);
            }
        }
        */

        
        float[,] colormap = new float[100, 3];
        
        int percent_1 = 33; int percent_2 = 66;
        for (var i = 20; i < 100; i++)
        {
            //print(i);
            colormap[i, 0] = Mathf.Clamp(System.Convert.ToSingle(i) / percent_1, 0, 1);
            if (i >= percent_1)
            {
                colormap[i, 1] = Mathf.Clamp((System.Convert.ToSingle(i) - percent_1) / (percent_2 - percent_1), 0, 1);
            }
            if (i >= percent_2)
            {
                colormap[i, 2] = Mathf.Clamp((System.Convert.ToSingle(i) - percent_2) / (100 - percent_2), 0, 1);
            }
            /*
            if (i >= 80)
            {
                colormap[i, 2] = 1;
            }
            */
        }
        



        //print(objColor.r + " " + objColor.g + " " + objColor.b + " " + objColor.a + " ");
        for (var i = 0; i < pointList.Count; i++)
		{
			// Get value in poinList at ith "row", in "column" Name
			float x = System.Convert.ToSingle(pointList[i][xName]);
			float y = System.Convert.ToSingle(pointList[i][yName]);
			float z = System.Convert.ToSingle(pointList[i][zName])*2;
            //float c = System.Convert.ToSingle(pointList[i][cName]);
            int c = Mathf.CeilToInt(System.Convert.ToSingle(pointList[i][cName])*100);
            //int c = Mathf.CeilToInt(System.Convert.ToSingle(pointList[i][cName]) * 255);
            float rsq = System.Convert.ToSingle(pointList[i][rName]);
            GameObject dataPoint = null;
            //instantiate the prefab with coordinates defined above
            dataPoint = Instantiate(Colored_sphere, new Vector3(x, y, z), Quaternion.identity);            
            Color Color_corrected_for_coef = objColor;
            //JetColorMap(c,0,1);
            //print(c);
            //print(Color_corrected_for_coef);         

            
            Color_corrected_for_coef.r = colormap[Mathf.Min(Mathf.Max(c-1,0),99), 0];
            Color_corrected_for_coef.g = colormap[Mathf.Min(Mathf.Max(c - 1, 0), 99), 1];
            Color_corrected_for_coef.b = colormap[Mathf.Min(Mathf.Max(c - 1, 0), 99), 2];
            
            Color_corrected_for_coef.a = System.Convert.ToSingle(Mathf.Max(c-1, 0)) /100;
            /*
            Color_corrected_for_coef.r = System.Convert.ToSingle(cividis_colormap[Mathf.Max(c, 0)][R_col]);
            Color_corrected_for_coef.g = System.Convert.ToSingle(cividis_colormap[Mathf.Max(c, 0)][G_col]);
            Color_corrected_for_coef.b = System.Convert.ToSingle(cividis_colormap[Mathf.Max(c, 0)][B_col]); ;            
            */
            dataPoint.GetComponent<Renderer>().material.color = Color_corrected_for_coef;
            //Make child of PointHolder
            dataPoint.transform.parent = PointHolder.transform;
            int factor_mult = 5;
            dataPoint.transform.localScale += new Vector3(rsq* factor_mult, rsq* factor_mult, rsq* factor_mult);

            //Make clone name with xyz
            string dataPointName = pointList[i][xName] + " " + pointList[i][yName] + " " + pointList[i][zName] + " " + pointList[i][cName];
			dataPoint.transform.name = dataPointName;

		}
	}

}