package com.example.a3d;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;

public class   Page extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_page);
    }
    public void startNewActivity(View v){
        Intent intent = new Intent(this, Main.class);
        startActivity(intent);
    }
    public void startNewActivity2(View v){
        Intent intent2 = new Intent(this, Favourites.class);
        startActivity(intent2);
    }
}