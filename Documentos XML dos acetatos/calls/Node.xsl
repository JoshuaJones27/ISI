<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" version="4.0" indent="yes"/>
  <xsl:strip-space elements="*"/>

  <xsl:template match="*">
    <xsl:apply-templates select="node()|@*"/>
  </xsl:template>

  <!--1-->
  
  <!--<xsl:template match="node()">
    <xsl:choose>
      <xsl:when test="processing-instruction()">Process Node:</xsl:when>
      <xsl:when test="self::comment()">Comment Node:<xsl:value-of select="."/></xsl:when>
      <xsl:otherwise>ELEMENT: <xsl:value-of select="name()"/> - <xsl:value-of select="text()"/><br/>
    </xsl:otherwise>
    </xsl:choose>
    
  <xsl:if test="processing-instruction()">Process</xsl:if>
    <xsl:if test="not(processing-instruction())">
      ELEMENT: <xsl:value-of select="name()"/>
    </xsl:if>
    <br/>
    <xsl:apply-templates/>
  </xsl:template>-->


  <!--2-->


  
  <xsl:template match="text()">
    ELEMENT: <xsl:value-of select="concat('&lt;',name(..),'> ',.,'&#xA;')"/>
    <br/>
  </xsl:template>

  <xsl:template match="@*">
    ATTR: <xsl:value-of select="concat(name(),'=&#x22;',.,'&#x22;&#xA;')"/><br/>
  </xsl:template>
  

</xsl:stylesheet>