# MedicationStringService RESTful API.

Author: Yeongjun Im (yeongjundev@gmail.com)

### API Running Commands

.gitignore is not created to ease your testing.

1. Run API _(at MedicationStringService.API directory)_

```
dotnet run
```

2. Run all unit tests _(at root directory)_

```
dotnet test
```

### Used Framework and Libraries

1. .Net Core 3.1 for Webapi framework
2. .Net EntityFramework as ORM
3. AutoMapper for data serialization and deserialization
4. Newtonsoft Json, JsonSchema for Json format validation
5. XUnit for unit testing

### Architect and Pattern Overview

1. Repository pattern
2. Unit of work pattern
3. MVC
4. Dependancy injection

### Endpoints:

1. [POST] http://localhost:5000/api/medicationstrings/
2. [GET] http://localhost:5000/api/statstics/

###### Endpoint demonstrations:

---

[POST] http://localhost:5000/api/medicationstrings/

Sample Request:

```javascript
{
	"medicationStrings": "186FASc73541_M_1058;18673cda541_S_0061;18673541_S_0146;18673cda541_XL_0056,18896541_M_0055;18896541_XXL_0038;aa1867354cc1_S_0073;18673541_L_0105;186735412333123121_NA_0073;18673543311_L_0105"
}
```

Sample Response: 200 OK

```javascript
[
  {
    id: 1,
    medicationId: '186FASc73541',
    bottleSize: 'M',
    dosageCount: 1058
  },
  {
    id: 2,
    medicationId: '18673cda541',
    bottleSize: 'S',
    dosageCount: 61
  },
  {
    id: 3,
    medicationId: '18673541',
    bottleSize: 'S',
    dosageCount: 146
  },
  {
    id: 4,
    medicationId: '18896541',
    bottleSize: 'XXL',
    dosageCount: 38
  },
  {
    id: 5,
    medicationId: 'aa1867354cc1',
    bottleSize: 'S',
    dosageCount: 73
  },
  {
    id: 6,
    medicationId: '18673541',
    bottleSize: 'L',
    dosageCount: 105
  },
  {
    id: 7,
    medicationId: '186735412333123121',
    bottleSize: 'NA',
    dosageCount: 73
  },
  {
    id: 8,
    medicationId: '18673543311',
    bottleSize: 'L',
    dosageCount: 105
  }
];
```

---

[GET] http://localhost:5000/api/statstics/

Sample Response: 200 OK

```javascript
{
    "totalCount": 8,
    "totalDosageCount": 1659,
    "perBottleSize": [
        {
            "bottleSize": "M",
            "count": 1
        },
        {
            "bottleSize": "S",
            "count": 3
        },
        {
            "bottleSize": "XXL",
            "count": 1
        },
        {
            "bottleSize": "L",
            "count": 2
        },
        {
            "bottleSize": "NA",
            "count": 1
        }
    ],
    "perMedicationId": [
        {
            "medicationId": "186FASc73541",
            "count": 1
        },
        {
            "medicationId": "18673cda541",
            "count": 1
        },
        {
            "medicationId": "18673541",
            "count": 2
        },
        {
            "medicationId": "18896541",
            "count": 1
        },
        {
            "medicationId": "aa1867354cc1",
            "count": 1
        },
        {
            "medicationId": "186735412333123121",
            "count": 1
        },
        {
            "medicationId": "18673543311",
            "count": 1
        }
    ]
}
```

---
