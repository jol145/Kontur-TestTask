<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" indent="yes"/>

	<!-- Ключ для группировки по имени и фамилии -->
	<xsl:key name="kEmployee" match="item" use="concat(@name, '|', @surname)"/>

	<xsl:template match="/">
		<Employees>
			<!-- Ищем всех уникальных сотрудников (item) во всем документе -->
			<xsl:for-each select="//item[generate-id() = generate-id(key('kEmployee', concat(@name, '|', @surname))[1])]">
				<Employee>
					<xsl:attribute name="name">
						<xsl:value-of select="@name"/>
					</xsl:attribute>
					<xsl:attribute name="surname">
						<xsl:value-of select="@surname"/>
					</xsl:attribute>

					<!-- Перечисляем все зарплаты этого сотрудника -->
					<xsl:for-each select="key('kEmployee', concat(@name, '|', @surname))">
						<salary>
							<xsl:attribute name="amount">
								<xsl:value-of select="@amount"/>
							</xsl:attribute>
							<xsl:attribute name="mount">
								<xsl:value-of select="@mount"/>
							</xsl:attribute>
						</salary>
					</xsl:for-each>
				</Employee>
			</xsl:for-each>
		</Employees>
	</xsl:template>
</xsl:stylesheet>
