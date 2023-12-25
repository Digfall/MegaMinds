package com.example.a3d;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.constraintlayout.widget.ConstraintLayout;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.database.Cursor;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.content.Intent;
import android.view.Menu;
import android.widget.ImageView;
import android.widget.TextView;

import java.io.IOException;
import java.util.ArrayList;

public class Main extends AppCompatActivity {
    RecyclerView rcView;

    DatabaseHelper myDB;

    ArrayList<String> defect_title, defect_description;
    ArrayList<byte[]> defect_image;

    ImageView empty_imageview;

    TextView no_data;

    DefectAdapter defectAdapter;


    @Override
    public boolean onOptionsItemSelected(MenuItem item)
    {
        if (item.getItemId() == R.id.item2)
        {
            Intent intent = new Intent(this, Main.class);
            startActivity(intent);

        } else if (item.getItemId() == R.id.item3)
        {
            Intent intent = new Intent(this, Favourites.class);
            startActivity(intent);

            {
                return super.onOptionsItemSelected(item);
            }

        }
        return true;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        
        rcView = findViewById(R.id.rcView);
        empty_imageview = findViewById(R.id.empty_imageview);
        no_data = findViewById(R.id.no_data);

        myDB = new DatabaseHelper(Main.this);
        myDB.createDatabase();
        defect_title = new ArrayList<>();
        defect_image = new ArrayList<>();
        defect_description = new ArrayList<>();

        storeDataInArrays();

        defectAdapter = new DefectAdapter(Main.this, this, defect_title, defect_image, defect_description);
        rcView.setAdapter(defectAdapter);
        rcView.setLayoutManager(new LinearLayoutManager(Main.this));




    }

    void storeDataInArrays(){
        Cursor cursor = myDB.readAllData();
        if(cursor.getCount() == 0) {
            empty_imageview.setVisibility(View.VISIBLE);
            no_data.setVisibility(View.VISIBLE);
        } else {
            while (cursor.moveToNext()) {
                defect_title.add(cursor.getString(0));
                defect_image.add(cursor.getBlob(1));
                defect_description.add(cursor.getString(2));
            }
            empty_imageview.setVisibility(View.GONE);
            no_data.setVisibility(View.GONE);
        }

    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.example_menu, menu);
        return super.onCreateOptionsMenu(menu);
    }

//    public boolean onOptionsItemSelected(MenuItem item) {
//        if(item.getItemId() == R.id.app_bar_search){
//
//        }
//        return super.onOptionsItemSelected(item);
//    }

    public void startNewActivity(View v){
        Intent intent = new Intent(this, Main.class);
        startActivity(intent);
    }
    public void startNewActivity2(View v){
        Intent intent2 = new Intent(this, Favourites.class);
        startActivity(intent2);
    }
    public void startNewActivity3(View v){
        Intent intent3 = new Intent(this, Page.class);
        startActivity(intent3);
    }
}