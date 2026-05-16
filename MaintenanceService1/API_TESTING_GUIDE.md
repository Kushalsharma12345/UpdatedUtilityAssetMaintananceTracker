# Utility Asset Maintenance Tracker - API Testing Guide

## Application Status
✅ **API is running on:** http://localhost:5151
✅ **Swagger UI:** http://localhost:5151/swagger
✅ **Database:** MaintenanceDB (SQL Server LocalDB)

## Quick Start with Postman

### Option 1: Import the Postman Collection
1. Open Postman
2. Click **Import** button (top left)
3. Select the file: `MaintenanceService1\Postman_Collection.json`
4. The collection "Utility Asset Maintenance Tracker API" will be added

### Option 2: Manual Testing with Postman

## API Endpoints

### 1. CREATE Maintenance Plan
**Method:** POST  
**URL:** `http://localhost:5151/api/maintenance-plans`  
**Headers:** Content-Type: application/json  
**Body (JSON):**
```json
{
  "assetId": 101,
  "frequency": "Monthly",
  "tasks": [
	{
	  "name": "Inspect Transformer",
	  "description": "Visual inspection of transformer for damage or leaks",
	  "estimatedHours": 2.5
	},
	{
	  "name": "Clean Equipment",
	  "description": "Remove dust and debris from equipment",
	  "estimatedHours": 1.0
	}
  ]
}
```

**Expected Response (200 OK):**
```json
{
  "planId": 1,
  "assetId": 101,
  "frequency": "Monthly",
  "tasks": [
	{
	  "taskId": 1,
	  "planId": 1,
	  "plan": null,
	  "name": "Inspect Transformer",
	  "description": "Visual inspection of transformer for damage or leaks",
	  "estimatedHours": 2.5
	},
	{
	  "taskId": 2,
	  "planId": 1,
	  "plan": null,
	  "name": "Clean Equipment",
	  "description": "Remove dust and debris from equipment",
	  "estimatedHours": 1.0
	}
  ]
}
```

---

### 2. GET All Maintenance Plans
**Method:** GET  
**URL:** `http://localhost:5151/api/maintenance-plans`  
**Headers:** None required

**Expected Response (200 OK):** Array of all maintenance plans

---

### 3. GET Maintenance Plans by Asset ID
**Method:** GET  
**URL:** `http://localhost:5151/api/maintenance-plans?assetId=101`  
**Headers:** None required

**Expected Response (200 OK):** Array of plans filtered by asset ID

---

### 4. UPDATE Maintenance Plan
**Method:** PUT  
**URL:** `http://localhost:5151/api/maintenance-plans/1`  
(Replace `1` with actual plan ID)  
**Headers:** Content-Type: application/json  
**Body (JSON):**
```json
{
  "assetId": 101,
  "frequency": "Quarterly",
  "tasks": [
	{
	  "name": "Full System Inspection",
	  "description": "Complete inspection of all components",
	  "estimatedHours": 5.0
	},
	{
	  "name": "Performance Testing",
	  "description": "Run performance tests on the system",
	  "estimatedHours": 3.5
	}
  ]
}
```

**Expected Response (200 OK):**
```json
"Updated successfully"
```

---

## Testing Scenarios

### Scenario 1: Basic CRUD Operations
1. **Create** a maintenance plan (POST)
2. **Get all** plans to verify it was created (GET)
3. **Get by Asset ID** to filter results (GET with query parameter)
4. **Update** the plan with new tasks (PUT)
5. **Get all** again to verify the update (GET)

### Scenario 2: Validation Testing
Test invalid frequency:
```json
{
  "assetId": 102,
  "frequency": "Weekly",
  "tasks": [
	{
	  "name": "Test Task",
	  "description": "Test",
	  "estimatedHours": 1.0
	}
  ]
}
```
**Expected:** 400 Bad Request with error message "Invalid frequency"

Test empty tasks:
```json
{
  "assetId": 102,
  "frequency": "Monthly",
  "tasks": []
}
```
**Expected:** 400 Bad Request with error message "At least one task is required"

### Scenario 3: Multiple Assets
1. Create plan for Asset ID 101 (Transformer)
2. Create plan for Asset ID 102 (Generator)
3. Create plan for Asset ID 103 (Substation)
4. Get all plans - should return 3
5. Filter by Asset ID 102 - should return only generator plan

---

## Swagger UI Testing
You can also test the API using the built-in Swagger UI:
1. Open browser: http://localhost:5151/swagger
2. Expand any endpoint
3. Click **Try it out**
4. Fill in the parameters
5. Click **Execute**
6. View the response

---

## Valid Values

### Frequency
- `"Monthly"` ✅
- `"Quarterly"` ✅
- Any other value ❌ (will return error)

### Asset IDs
- Any positive integer (e.g., 101, 102, 103)

### Estimated Hours
- Any positive number (e.g., 1.0, 2.5, 5.0)

---

## Database Verification
To verify data is being stored correctly:
1. Open SQL Server Management Studio or Azure Data Studio
2. Connect to: `(localdb)\mssqllocaldb`
3. Navigate to: `MaintenanceDB` database
4. Query tables:
   - `MaintenancePlans`
   - `Tasks`

```sql
-- View all plans
SELECT * FROM MaintenancePlans;

-- View all tasks with their plans
SELECT t.*, p.Frequency, p.AssetId 
FROM Tasks t 
INNER JOIN MaintenancePlans p ON t.PlanId = p.PlanId;
```

---

## Troubleshooting

### API Not Running
- Check the terminal for error messages
- Verify port 5151 is not in use by another application

### Connection String Issues
- Verify SQL Server LocalDB is installed
- Check connection string in `appsettings.json`

### 404 Not Found
- Verify the URL is correct: `http://localhost:5151/api/maintenance-plans`
- Ensure the API is running

### 500 Internal Server Error
- Check the API console for detailed error messages
- Verify database connection is working

---

## Stop the Application
To stop the API server:
- Press `Ctrl+C` in the terminal where the app is running

## Restart the Application
```powershell
cd MaintenanceService1
dotnet run
```
