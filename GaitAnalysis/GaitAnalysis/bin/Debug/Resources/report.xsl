<?xml version="1.0" encoding="ISO-8859-1"?>


<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">

    <html>
      <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"/>

      <head>
        <title>Gait Report</title>
        <link rel="stylesheet" href="resources/Default.css" type="text/css"/>
      </head>

      <body background="resources/background.gif" >
        <a name="top"/>
       
        <BR></BR>

        <!-- Start of Dashboard Section -->
        <div id="summSection">
			
	    </div>
	    <!--End of Dashboard Section-->		  
          
          <!--Start of Summary of Person information section-->
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
              <td>Timestamp:</td>
              <td style="text-align:left;">
				<xsl:value-of select="Report/summary/timestamp/@date"/>&#160;<xsl:value-of select="report/summary/timestamp/@value"/>
			  </td>
            </tr>
			<xsl:if test="report/summary/status/@score">
                <tr>
                  <td>
                    Score:
                  </td>
                  <td style="text-align:left;">
                    <xsl:value-of select="report/summary/status/@score"/>
                  </td>
                </tr>
            </xsl:if>
            <tr class="borderBottom">
              <td>Standard:</td>
              <td style="text-align:left;">
				<xsl:value-of select="report/summary/standard/@value"/>
			  </td>
            </tr>
         </table>
         </div>
		 <!--End of General Information-->
         
         <!-- Start of Summary of Comments Section-->
         <div id="summComments">
         <table>
            <tr>
              <th>Comments</th>
            </tr>
			<xsl:for-each select="report/user_comments/comment_line">
                <tr>
					<td style="text-align:left;"><xsl:value-of select="@value"/>&#160;
                    </td>
                </tr>
            </xsl:for-each>			
          </table>
        </div>
		<!--End of Comments Section-->
        
        <!--Start of Data Grids-->
        <div id="detailsSection">
			<!--Standard requirements section-->
			<h2><a href="#top" title="Go to top">Standard Requirements</a></h2>
			<table width="700px">
				<tr>
				  <th class="txt-center">No.</th>
				  <th>Requirement Name</th>
				  <th>Attributes</th>
				  <th>Behavior</th>	
				  <th>Score</th>				  
				 <!--<xsl:if test="report/summary/status/@score">
					<th>Score</th>
				  </xsl:if>-->
				</tr>
				
				<!--Start of Standard requirement generated output-->
				<xsl:for-each select="report/selected_requirements/requirement">
					<tr>
						<!--Order Number-->
						<td class="txt-center"><a>
							<xsl:attribute name="name">
							<xsl:value-of select="position()"/>
							</xsl:attribute>
							<xsl:value-of select="position()"/>
						  </a>
						</td>
						<!--Requirement Name-->
						<td>
							<xsl:value-of select="@local_name"/>
						</td>
						<!--Attributes-->
						<td>
							<xsl:for-each select="attribute">
								<xsl:value-of select="@local_name"/>=<xsl:value-of select="@value"/>&#160;
							</xsl:for-each>
						  &#160;
						</td>
						<!--Behavior-->
						<td>
							<xsl:value-of select="@type"/>
						</td>
						<!--Score-->
							 <td>
                      <xsl:value-of select="@score"/>
                    </td>
						<!--<xsl:if test="report/summary/status/@score">
							<td>
								<xsl:value-of select="report/summary/status/@score"/>
							</td>
						</xsl:if>-->
					</tr>
				</xsl:for-each>
				<!--End Standard reequirement output section-->			
			</table>

			<!--Failed requirement section-->
			<h2><a href="#top" name="Failed Requirements" title="Go to top">Failed Requirements</a></h2>
			<table width="700px">
				<tr>
				  <th>Requirement Name</th>
				  <th>#</th>
				  <th>Candidate Name</th>
				  <th>Reason</th>
				</tr>				
				<!--Failed Requirements Generated Output-->
				<xsl:for-each select="report/results/result[@status='Fail']">
					<xsl:sort select="requirement/@local_name"/>
					<xsl:sort select="requirement/@application_ref"/>
					<xsl:sort select="candidate/@name"/>				
					<tr>
						<!--Requirement name-->
						<td>
							 <xsl:value-of select="requirement/@local_name"/>
						</td>						
						<!-- Requirement instance number with link to Requirement table above -->
						<xsl:variable name="appl_name" select="requirement/@application_ref"/>
						<td>
						  <xsl:for-each select="../../selected_requirements/requirement">
							<xsl:if test="@application_ref = $appl_name">
							  <a>
								<xsl:attribute name="href">
								  #<xsl:value-of select="position()"/>
								</xsl:attribute>
								<xsl:value-of select="position()"/>
							  </a>
							</xsl:if>
						  </xsl:for-each>
						</td>						
						<!-- Candidate type -->
						<td>
						  <xsl:value-of select="candidate/@name"/>
						</td>
						<!-- Reason -->
						<td>
						  <xsl:for-each select="message">
							<xsl:value-of select="@text"/>&#160;
						  </xsl:for-each>
						  &#160;
						</td>
					</tr >
				</xsl:for-each>
				<!--End of Failed Requirements Generated Output-->
			</table>

			<!--Passed requirements secion-->
			<h2><a href="#top" name="Passed Requirements" title="Go to top">Passed Requirements</a></h2>
			<table width="700px">
				<tr>
				  <th>Requirement Name</th>
				  <th>#</th>
				  <th>Candidate Name</th>
				</tr>
				<!--Start of Passed Requirements Generated Data Section-->
				<xsl:for-each select="report/results/result[@status='Pass']">
					<xsl:sort select="requirement/@local_name"/>
					<xsl:sort select="requirement/@application_ref"/>
					<xsl:sort select="candidate/@name"/>
					<tr>
						<!-- Requirement -->
						<td>
						  <xsl:value-of select="requirement/@local_name"/>
						</td>
						<!-- Requirement instance number with link to Requirement table above -->
						<xsl:variable name="appl_name" select="requirement/@application_ref"/>
						<td>
						  <xsl:for-each select="../../selected_requirements/requirement">
							<xsl:if test="@application_ref = $appl_name">
							  <a>
								<xsl:attribute name="href">
								  #<xsl:value-of select="position()"/>
								</xsl:attribute>
								<xsl:value-of select="position()"/>
							  </a>
							</xsl:if>
						  </xsl:for-each>
						</td>
						<!-- Candidate -->
						<td>
						  <xsl:value-of select="candidate/@name"/>
						</td>
					</tr>
                </xsl:for-each>
				<!--End of Passed Requirements Generated Data Secction-->
			</table>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
