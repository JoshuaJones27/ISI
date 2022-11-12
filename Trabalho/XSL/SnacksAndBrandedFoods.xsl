<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
  <html>
  <body>
  <h2>Snacks and Branded Foods</h2>
  <table border="1">
    <tr bgcolor="#9acd32">
      <th>Product Name</th>
      <th>Brand</th>
      <th>Price</th>
      <th>Discount Price</th>
      <th>Quantity</th>
      <th>Category</th>
      <th>SubCategory</th>
    </tr>
    <xsl:for-each select="Rows/Row">
    <tr>
      <td><xsl:value-of select="ProductName"/></td>
      <td><xsl:value-of select="Brand"/></td>
      <td><xsl:value-of select="Price"/></td>
      <td><xsl:value-of select="DiscountPrice"/></td>
      <td><xsl:value-of select="Quantity"/></td>
      <td><xsl:value-of select="Category"/></td>
      <td><xsl:value-of select="SubCategory"/></td>
    </tr>
    </xsl:for-each>
  </table>
  </body>
  </html>
</xsl:template>

</xsl:stylesheet>