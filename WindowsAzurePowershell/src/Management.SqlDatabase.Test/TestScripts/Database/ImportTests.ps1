﻿# ----------------------------------------------------------------------------------
# Copyright Microsoft Corporation
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
# ----------------------------------------------------------------------------------

function TestImportWithRequestObject
{
    ####################################################
    # Import Database
	
	$status = $null
    
    ###########
	# Test the first parameter set

    $BlobName = $DatabaseName1 + ".bacpac"
    $dbName = $DatabaseName1 + "-import1"
	Write-Output "Importing from Blob: $BlobName"

	$Request = Start-AzureSqlDatabaseImport -SqlConnectionContext $context -StorageContainer $container `
		-DatabaseName $dbName -BlobName $BlobName
    Assert {$Request} "Failed to initiate the first import opertaion"
	$id = ($Request.RequestGuid)
    Write-Output "Request Id for import1: $id"

    ##############
    # Test Get IE status with request object
	do
	{
		Start-Sleep -m 1500
		$status = Get-AzureSqlDatabaseImportExportStatus $Request
		$s = $status.Status
		Write-Output "Request Status: $($status.Status)"
	} while($status.Status -ne "Completed")
}

function TestImportWithRequestObjectAndOptionalParameters
{
    ####################################################
    # Import Database
	$status = $null
    
    ###########
	# Test optional Edition
    
    $BlobName = $DatabaseName1 + ".bacpac"
	Write-Output "Importing from Blob: $BlobName"

    for( $i = 0; $i -ne 3; $i++)
    {
        Write-Output "Running test parameter set combination $i"
        $dbName = $DatabaseName1 + "Options-$i"
        Write-Output "Database name: $dbName"

        $Request = $null

        if ($i -eq 0)
        {
            $currEdition = "Business"
	    
            $Request = Start-AzureSqlDatabaseImport -SqlConnectionContext $context -StorageContainer $container `
		        -DatabaseName $dbName -BlobName $BlobName -Edition $currEdition
        }
        elseif ($i -eq 1)
        {
            $currMaxSize = 5
	    
            $Request = Start-AzureSqlDatabaseImport -SqlConnectionContext $context -StorageContainer $container `
		        -DatabaseName $dbName -BlobName $BlobName -DatabaseMaxSize $currMaxSize
        }
        elseif ($i -eq 2)
        {
            $currEdition = "Business"
            $currMaxSize = 20
	    
            $Request = Start-AzureSqlDatabaseImport -SqlConnectionContext $context -StorageContainer $container `
		        -DatabaseName $dbName -BlobName $BlobName -Edition $currEdition -DatabaseMaxSize $currMaxSize
        }

        Assert {$Request} "Failed to initiate the import opertaion for parameter set combination: $i"
	    $id = ($Request.RequestGuid)
        Write-Output "Request Id for import: $id"
    
        ##############
        # Test Get IE status with request object
	    do
	    {
		    Start-Sleep -m 1500
		    $status = Get-AzureSqlDatabaseImportExportStatus $Request
		    $s = $status.Status
		    Write-Output "Request Status: $($status.Status)"
            if($status.Status -eq "Failed")
            {
		        Write-Output "Error message: $($status.ErrorMessage)"
                break
            }
	    } while($status.Status -ne "Completed")
    }
}

function TestImportWithRequestId
{
    ###########
	# Test the second parameter set

    $BlobName2 = $DatabaseName2 + ".bacpac"
    $dbName = $DatabaseName2 + "-import3"
	Write-Output "Importing from Blob: $BlobName2"

	$Request = Start-AzureSqlDatabaseImport -SqlConnectionContext $context -StorageContext $StgCtx `
		-StorageContainerName $ContainerName -DatabaseName $dbName -BlobName $BlobName2
    Assert {$Request} "Failed to initiate the third import opertaion"
	$id = ($Request.RequestGuid)
    Write-Output "Request Id for Import: $id"
    
    ##############
    # Test Get IE status with request id, servername, and login credentials
	do
	{
		Start-Sleep -m 1500
		$status = Get-AzureSqlDatabaseImportExportStatus -RequestId $Request.RequestGuid `
            -ServerName $server.ServerName -UserName $UserName -Password $Password
		$s = $status.Status
		Write-Output "Request Status: $s"
	} while($status.Status -ne "Completed")
}