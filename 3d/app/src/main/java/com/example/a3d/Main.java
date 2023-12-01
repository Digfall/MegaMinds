package com.example.a3d;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.content.Intent;

public class Main extends AppCompatActivity {


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }
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