<?php
mysql_connect("192.99.38.184", "johnbob332", "johnbob332GG");
mysql_select_db("localhostdb");

  $query = "SELECT Server_Name, IP, Port, Game, Players FROM servers_info";

  $result = mysql_query($query);

  // If results were found, output them
  if (mysql_num_rows($result) > 0) {
 
 while($info = mysql_fetch_array( $result )) 
 { 
   echo "".$info['Server_Name'] . " ";
   echo "".$info['IP'] . " ";
   echo "".$info['Port'] . " ";
   echo "".$info['Game'] . " ";
   echo "".$info['Players'] . " ";
 
 } 
  } else {
    echo ("invalid id..!");
  }

?>