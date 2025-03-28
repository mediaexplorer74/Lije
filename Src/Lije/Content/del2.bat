@for /f "delims= eol=" %%f in ('dir/a-d-h-s/b/s^|findstr "\.[^\\][^\\]*\.[^\\][^\\]*$"') do @ren "%%f" *.
