#!/bin/bash

cat `ls | grep '^[0-9][0-9][0-9]_.*\.sql$'` > all.sql

