<?xml version="1.0" encoding="ISO-8859-1"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">

    <html>
      <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"/>
      <head>
        <title>Gait Analysis Report</title>
        <link rel="stylesheet" href="Resources/Default.css" type="text/css"/>
      </head>

      <body>
        <!-- Table for report header -->
        <table width="700px">
          <tr>
            <td align="center">
              <img src="Resources/BitsLogo.png" />
            </td>
          </tr>
          <tr>
            <td class="headerTitle" align="Center">
              Gait Analysis Report
            </td>
          </tr>
        </table>
        <!-- End of table for report header -->
        <br></br>

       
 
        <!--End of General Information-->
       <div id="summGeneral">
          <table>
            <tr>
              <th colspan="2">Person Information</th>
            </tr>
            <tr>
              <td class="summGeneralLabel">Name:</td>
              <td class="summGeneralDesc" style="text-align:left;">
                <xsl:value-of select="report/summary/name/@value"/>
              </td>
            </tr>
			<tr>
              <td class="summGeneralLabel">Weight:</td>
              <td class="summGeneralDesc" style="text-align:left;">
                <xsl:value-of select="report/summary/weight/@value"/>
              </td>
			  <td class="summGeneralLabel">Height:</td>
              <td class="summGeneralDesc" style="text-align:left;">
                <xsl:value-of select="report/summary/height/@value"/>
              </td>
            </tr>
			<tr>
              <td class="summGeneralLabel">Age:</td>
              <td class="summGeneralDesc" style="text-align:left;">
                <xsl:value-of select="report/summary/age/@value"/>
              </td>
			  <td class="summGeneralLabel">Gender:</td>
              <td class="summGeneralDesc" style="text-align:left;">
                <xsl:value-of select="report/summary/gender/@value"/>
              </td>
            </tr>
			<tr>
              <td class="summGeneralLabel">Kinect Version:</td>
              <td class="summGeneralDesc" style="text-align:left;">
                <xsl:value-of select="report/summary/kinectVersion/@value"/>
              </td>
			  <td class="summGeneralLabel">Timestamp:</td>
              <td style="text-align:left;">
                <xsl:value-of select="report/summary/timestamp/@value"/>
              </td>
            </tr>
            
            <tr class="borderBottom">
              <td>Folder Path:</td>
              <td style="text-align:left;">
                <xsl:value-of select="report/summary/folderPath/@value"/>
              </td>
            </tr>
          </table>
        </div>
		
		
		
	<!--Start of Data Grids-->
        <div id="detailsSection">
        <!-- Start of standard section-->
            <div id="detailsSection">               
                <h2>
                    <a href="#top" title="Go to top">Gait Values</a>
                </h2>
                <table width="700px">
					<tr>					
					  <td colspan="2"><b>Age Group:  </b>
						<xsl:value-of select="report/standardGaitParameters/agegroup/@std_value"/>
					  </td>
					</tr>
                    <tr>
                        <th class="txt-center">Observed Values</th>                        
                        <th>Standard Values</th>                        
                    </tr>
                    <!-- Requirements Generated Output-->
					
                     <tr>
						<td><b>Stride Length:</b>
							<br></br>
							<xsl:value-of select="report/observedGaitParameters/stridelength/@obsv_value"/>
						</td>
						<td style="text-align:center;">
							<xsl:value-of select="report/standardGaitParameters/stridelength/@std_value"/>
						</td>
					</tr>     
					<tr>
						<td><b>Walking Base:</b>
							<br></br>
							<xsl:value-of select="report/observedGaitParameters/walkingbase/@obsv_value"/>
						</td>
						<td style="text-align:center;">
							<br></br>
							<xsl:value-of select="report/standardGaitParameters/walkingbase/@std_value"/>
						</td>
					</tr>    
					<tr>
						<td><b>Speed of Walking:</b>
							<br></br>
							<xsl:value-of select="report/observedGaitParameters/speedofwalking/@obsv_value"/>
						</td>
						<td style="text-align:center;">
							<br></br>
							<xsl:value-of select="report/standardGaitParameters/speedofwalking/@std_value"/>
						</td>
					</tr>
					<tr>
						<td><b> Cadence:</b>
							<br></br>
							<xsl:value-of select="report/observedGaitParameters/cadence/@obsv_value"/>
						</td>
						<td style="text-align:center;">
							<br></br>
							<xsl:value-of select="report/standardGaitParameters/cadence/@std_value"/>
						</td>
					</tr>
					<tr>
						<td><b>Step Factor:</b>
							<br></br>
							<xsl:value-of select="report/observedGaitParameters/stepfactor/@obsv_value"/>
						</td>
						<td style="text-align:center;">
							<br></br>
							<xsl:value-of select="report/standardGaitParameters/stepfactor/@std_value"/>
						</td>
					</tr>
					<tr>
						<td><b> Cycle Time:</b>
							<br></br>
							<xsl:value-of select="report/observedGaitParameters/cycletime/@obsv_value"/>
						</td>
						<td style="text-align:center;">
							<br></br>
							<xsl:value-of select="report/standardGaitParameters/cycletime/@std_value"/>
						</td>
					</tr>
					
                    <!--End of Standards Output-->
                </table>
            </div>                       
        </div>		
		 <div id="summSection">  
			<br></br><br></br>		 
			Signature:
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>