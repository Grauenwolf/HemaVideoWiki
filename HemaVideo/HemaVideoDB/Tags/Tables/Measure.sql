CREATE TABLE Tags.Measure
(
	MeasureKey INT NOT NULL CONSTRAINT PK_Measure PRIMARY KEY,
	MeasureName nvarchar(50) NOT NULL CONSTRAINT UK_Measure_MeasureName UNIQUE,
	AlternateMeasureName NVARCHAR(50) NOT NULL
)
