namespace Disk.Repository.Functions
{
    public class PatientFunction
    {
        //GetPatientsByAddress
        /*CREATE FUNCTION GetPatientsByAddress(
    @addressId INTEGER
)
RETURNS TABLE
AS
RETURN
    SELECT*
    FROM patient
    WHERE pat_address = @addressId;*/

        // GetPatientCountByRegion
        /*        CREATE FUNCTION GetPatientCountByRegion(
            @regionId INTEGER
        )
        RETURNS INTEGER
        AS
        BEGIN
            DECLARE @count INTEGER;
            SELECT @count = COUNT(*)
            FROM patient p
            JOIN address a ON p.pat_address = a.addr_id
            JOIN district d ON a.addr_region = d.dst_id
            WHERE d.dst_region = @regionId;
                RETURN @count;
                END;*/
    }
}
