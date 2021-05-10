CREATE FUNCTION [dbo].[f_Payment_FullDescription] 
(
@Amount decimal(18, 4),
@PaymentType [nvarchar](50),
@BankName [nvarchar](255),
@ChequeNumber [nvarchar](255),
@BrunchNumber [nvarchar](255),
@TransitNumber [nvarchar](255),
@AccountNumber [nvarchar](255),
@TransactionId [nvarchar](255),
@Notes [nvarchar](max)
) RETURNS [nvarchar](max)
AS
BEGIN

Declare @ret VARCHAR(4000)
set @ret = '';

if (@Amount is not null)
	set @ret = @ret + case when @ret='' then '' else char(10) end + 'Amount: ' + cast(cast(@Amount as decimal(18,2)) as nvarchar(255));

if (@PaymentType <> '')
	set @ret = @ret + case when @ret='' then '' else char(10) end + 'Payment Type: ' + @PaymentType;

if (@BankName <> '')
	set @ret = @ret + case when @ret='' then '' else char(10) end + 'Bank: ' + @BankName;

if (@ChequeNumber <> '')
	set @ret = @ret + case when @ret='' then '' else char(10) end + 'Cheque Number: ' + @ChequeNumber;

if (@BrunchNumber <> '')
	set @ret = @ret + case when @ret='' then '' else char(10) end + 'Brunch Number: ' + @BrunchNumber;

if (@TransitNumber <> '')
	set @ret = @ret + case when @ret='' then '' else char(10) end + 'Transit Number: ' + @TransitNumber;

if (@AccountNumber <> '')
	set @ret = @ret + case when @ret='' then '' else char(10) end + 'Account Number: ' + @AccountNumber;

if (@TransactionId <> '')
	set @ret = @ret + case when @ret='' then '' else char(10) end + 'Transaction Id: ' + @TransactionId;

if (@Notes <> '')
	set @ret = @ret + case when @ret='' then '' else char(10) end + 'Notes: ' + @Notes;


--alter table Payments drop column FullDescription
--alter table Payments add FullDescription as dbo.f_Payment_FullDescription(Amount, PaymentType,BankName,ChequeNumber,BrunchNumber,TransitNumber,AccountNumber,TransactionId,Notes)
--select FullDescription, * from Payments
return @ret;


END