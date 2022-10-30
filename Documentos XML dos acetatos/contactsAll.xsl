<?xml version="1.0" encoding="iso-8859-1"?>
<!-- 
by lufer 
contacts.xsl
Manipulação de várias expressões XPath/Templates
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:output method="html" indent="yes" encoding="iso-8859-1"/>

<xsl:template match="/">
	<html>
    <body>
      <h2>Resultado:</h2>

      <!--<xsl:apply-templates/>-->
      <!--<xsl:apply-templates select="//Company"/>-->
      <xsl:apply-templates select="Companies"/>
      
      <h3>FIM</h3>
    </body>
	</html>
</xsl:template>


<!-- 2 -->

<!--<xsl:template match="Phone">
	--><!--<b>Phone= <xsl:value-of select="."/></b>--><!--
	<xsl:value-of select="text()"/><br/>
</xsl:template>


<xsl:template match="Person/Name">
	Person Name= <i><xsl:value-of select="."/></i><br/>
</xsl:template>-->


<!-- 7
<xsl:template match="Sales">
	<b>SALES:</b><br/>
	<i>
	<xsl:apply-templates select="Person"/>
	</i>
</xsl:template>


<xsl:template match="Billing">
	<b>BILLING:</b><br/>
	<u>
	<xsl:apply-templates select="Person"/>
	</u>
</xsl:template>

-->

<!-- 4
<xsl:template match="Person">
	Person Id= <i><xsl:value-of select="@id"/></i><br/>
	Person Name= <i><xsl:value-of select="Name"/></i><br/>
	Person Phone= <i><xsl:value-of select="Phone"/></i><br/>
</xsl:template>
-->

<!-- 5
<xsl:template match="Companies">
	Companie:<xsl:value-of select="Company/@id"/>
	<xsl:apply-templates/>
</xsl:template>

-->

<!-- 6
<xsl:template match="Contacts">
	Contacts:
	<xsl:apply-templates/>	
</xsl:template>
-->


<!-- 8 
<xsl:template match="Contacts/*/Person">
	Person Name= <xsl:value-of select="Name"/><br/>
</xsl:template>
-->

<!-- 9
<xsl:template match="Phone/text()">
	<br/>Phone= <xsl:value-of select="."/>
</xsl:template>
-->

<!-- 10
<xsl:template match="Company[2]">
	<br/>Company Name= <xsl:value-of select="Name"/>
</xsl:template>
-->



<xsl:template match="//Person[NEW]">
	<br/>Person Name= <xsl:value-of select="Name"/> Person Phone= <xsl:value-of select="Phone"/>
</xsl:template>



<!-- 12-->

<xsl:template match="Companies">
	<xsl:for-each select="Company">
		<br/>ID = <xsl:value-of select="@id"/>
		<!--<xsl:apply-templates/>-->
	</xsl:for-each>
</xsl:template>




<!--  13 -->

<!--<xsl:template match="Company">
	<p>Companhia: <b><xsl:value-of select="Name"/></b></p>
	--><!--<ul><xsl:apply-templates select="Contacts//Name"/></ul>--><!--
	<ul><xsl:apply-templates/></ul>
</xsl:template>

<xsl:template match="Contacts/Name">
	<li><i><xsl:value-of select="."/></i></li>
</xsl:template>-->
<!-- -->


<xsl:template match="Companies">
	<xsl:for-each select="Company">
		<p>Company:<b><xsl:value-of select="Name/text()"/>
    <br/>Primary: <xsl:value-of select="Contacts/Primary[Person/@id=1]/Person/Name/text()"/>
		<br/>ID=<xsl:value-of select="@id"/>
		<br/>Gente: <xsl:value-of select="count(Contacts//Person)"/>
		</b></p>
	</xsl:for-each>
</xsl:template>


<!--OverLoading: Redefinição de Template. O último a definir prevalece!-->

<!--<xsl:template match="Companies">
	<xsl:for-each select="Company">
		--><!--<xsl:sort select="Name" order="descending"/>--><!--	--><!--Ordenar--><!--
		<p>Company:<b>
		<xsl:value-of select="Name"/>
		</b></p>
		<ul>
		<xsl:for-each select="Contacts//Name">
			<li>
			<xsl:value-of select="."/>
			<xsl:if test="../NEW">
				<i> - (nova entrada)</i>
			</xsl:if>
			 --><!--- <xsl:apply-templates select="../Phone"/>--><!--
			</li>
		</xsl:for-each>
		</ul>
	</xsl:for-each>
</xsl:template>-->




</xsl:stylesheet>

