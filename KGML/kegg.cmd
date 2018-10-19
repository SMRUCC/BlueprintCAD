@echo off

REM download kegg reaction data model
REM
REM outputdir=br08201
kegg_tools /Download.Reaction
REM download kegg compound data model
REM
REM outputdir=KEGG_cpd
kegg_tools /Download.Compounds
REM download kegg pathway data model
REM
REM outputdir=br08901
kegg_tools /Pathways.Downloads.All
REM dump all of the kegg pathway map data for the 
REM data visualization of the kegg enrichment result.
REM
REM outputdir=br08901_pathwayMaps
kegg_tools /dump.kegg.maps