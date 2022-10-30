<?xml version="1.0"?>
<!--
by lufer
2018
-->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" >
<xsl:output method="html"/>
  
  <xsl:template match="/">
    <STYLE>
      h1: {color: red font-family: Arial; }
      body {color: red; font-family: Arial; font-size: 8pt;}
      tr.clsOdd { background-color: beige;  }
      tr.clsEven { background-color: #cccccc; }
    </STYLE>
    <h1>Início</h1>
   <xsl:apply-templates/>
    <h3>Fim</h3>

    <xsl:for-each select="//*">
      <xsl:value-of select="node()"/>
    </xsl:for-each>
  </xsl:template>
  
  <xsl:template match="PHONE_RECORDS">
    <html>
      <head>
        <title>Phone Listing</title>
      </head>
      <body>
        <h1>Phone Call Records</h1>
        <table border="1">
          
          <!--ANALISAR
          <th>Item</th>
          <xsl:for-each select="CALL[1]/*">
            <th>
              <xsl:value-of select="name()"/>
            </th>
          </xsl:for-each>-->
          <th>Item</th>
          <th>Source Number</th>
          <th>Destination Number</th>
          <th>Date (MM/DD/YY)</th>
          <th>Duration</th>
          <!--<xsl:apply-templates/>-->
		  <xsl:apply-templates select="CALL"/>
		  
		  <!--
		  <xsl:for-each select="CALL">
			...
		  </xsl:for-each>
		  -->
        </table>
      </body>
    </html>
  </xsl:template>

  
  <xsl:template match="CALL">
      <tr>
        <xsl:if test="position() mod 2 = 1">
          <xsl:attribute name='class'>clsOdd</xsl:attribute>
        </xsl:if>
        <xsl:if test="position() mod 2 != 1">
          <xsl:attribute name='class'>clsEven</xsl:attribute>
        </xsl:if>
      <td>
        <xsl:number/>
        <!-- ou
        <xsl:value-of select="position()"/>
        -->
      </td>	 
      <td>
        <xsl:value-of select="FROM/text()"/>
      </td>
      <td>
        <xsl:value-of select="DESTINATION/text()"/>
      </td>
      <td>
        <xsl:value-of select="DATE"/>
      </td>
      <td>
          <xsl:value-of select="DURATION/@HOURS"/>
      </td>
    </tr>
  </xsl:template>

</xsl:stylesheet>

