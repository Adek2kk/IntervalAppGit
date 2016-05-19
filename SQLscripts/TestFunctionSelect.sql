SELECT ro.roomNumber  AS KEY, ti.timet AS START_INTERVAL,
  LEAD(ti.timet) OVER(ORDER BY ro.roomNumber, ti.timet)  AS END_INTERVAL,
  REGR_SLOPE(en.measurevalue,TO_NUMBER(TO_CHAR(ti.timet,'YYYYMMDD')) ) OVER(ORDER BY ro.roomNumber, ti.timet ROWS BETWEEN CURRENT ROW AND 1 FOLLOWING)     AS SLOPE,
  REGR_INTERCEPT(en.measurevalue,TO_NUMBER(TO_CHAR(ti.timet,'YYYYMMDD'))) OVER(ORDER BY ro.roomNumber, ti.timet ROWS BETWEEN CURRENT ROW AND 1 FOLLOWING) AS INTERCEPT
  FROM TES_FACT_ENERGY en join TES_DIMENSION_TIME ti on ti.id=en.id_time join TES_DIMENSION_ROOM ro on ro.id=en.id_room
  WHERE ro.roomnumber = 2;