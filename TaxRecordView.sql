ALTER VIEW TaxRecordView AS
SELECT 
    tp.taxPayerID AS TaxId,
    tp.[group] AS TaxPayerType,
	SUM(t.amount) As PaymentAmount,
    MIN(CASE 
        WHEN t.settled = 1 THEN 1
        ELSE 0
    END) AS hasPaid,
    MAX(tp.amountOwing) AS AmountOwing,
    tt.description AS TaxType
FROM 
    TaxPayment t
RIGHT JOIN 
    TaxPayer tp ON tp.id = t.taxPayerID
INNER JOIN 
    TaxPayerType tpt ON tp.[group] = tpt.id
INNER JOIN 
    TaxType tt ON t.taxType = tt.id
GROUP BY 
   tp.taxPayerID,
   tt.description,
   tp.[group];