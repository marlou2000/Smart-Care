<?php
    require_once "conn.php";
    require_once "validate.php";
    
    $sql0 = "SELECT * FROM current_ids_admin";
    $result0 = mysqli_query($conn, $sql0);

    if($result0){
        while($row1=mysqli_fetch_assoc($result0)){
            $sql = "SELECT * FROM patient WHERE patient_id='$row1[current_patient_clicked_id]'";
            $result = mysqli_query($conn, $sql);

            if($result){
                while($row=mysqli_fetch_assoc($result)){
                    $sql1 = "SELECT * FROM user WHERE user_id ='$row[encoder]'";
                    $result1 = mysqli_query($conn, $sql1);

                    if($result1){
                        while($row2=mysqli_fetch_assoc($result1)){
                            $sql2= "SELECT * FROM user WHERE username != 'admin' and user_id != '$row[encoder]' ORDER BY staff_name ASC";
                            $result2 = mysqli_query($conn, $sql2);

                            if($result2){
                                while($row3=mysqli_fetch_assoc($result2)){
                                    echo $row3['staff_name'].'`';
                                }
                            }

                            else{
                                echo 'failure'; 
                            }
                        }
                    }

                    else{
                        echo 'failure';
                    }
                } 
            }
        
            else{
                echo 'failure';
            }
        } 
    }

    else{
        echo 'failure';
    }

    
    
?>