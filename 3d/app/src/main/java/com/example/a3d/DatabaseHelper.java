package com.example.a3d;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;
import android.widget.Toast;
import androidx.annotation.Nullable;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.nio.file.Files;
import java.nio.file.Paths;

class DatabaseHelper extends SQLiteOpenHelper {

    private Context context;
    private static final String DATABASE_NAME = "printing_defects.db";
    private static final int DATABASE_VERSION = 1;

    private static final String TABLE_NAME = "Defects";
    private static final String COLUMN_TITLE = "Title";
    private static final String COLUMN_IMAGE = "Image";
    private static final String COLUMN_DESCRIPTION = "Description";
    private static final String COLUMN_CAUSES = "Causes";
    private static final String COLUMN_SOLUTIONS = "Solutions";

    DatabaseHelper(@Nullable Context context) {
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
        this.context = context;
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        String query = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME +
                " (" + COLUMN_TITLE + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                COLUMN_IMAGE + " BLOB, " +
                COLUMN_DESCRIPTION + " TEXT, " +
                COLUMN_CAUSES + " TEXT, " +
                COLUMN_SOLUTIONS + " TEXT);";
        db.execSQL(query);
    }
    @Override
    public void onUpgrade(SQLiteDatabase db, int i, int i1) {
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_NAME);
        onCreate(db);
    }

    public void createDatabase() {
        boolean databaseExists = checkDatabase();

        if(!databaseExists) {
            this.getReadableDatabase();
            try {
                copyDatabase();
            } catch (IOException e) {
                throw new Error("Error copying database");
            }
        }
    }

    private boolean checkDatabase() {
        File databasePath = context.getDatabasePath(DATABASE_NAME);
        return databasePath.exists();
    }

    private void copyDatabase() throws IOException {
        InputStream inputStream = context.getAssets().open("databases/" + DATABASE_NAME);
        String outFileName = context.getDatabasePath(DATABASE_NAME).getAbsolutePath();
        OutputStream outputStream = Files.newOutputStream(Paths.get(outFileName));

        byte[] buffer = new byte[4096];
        int length;
        while ((length = inputStream.read(buffer)) > 0) {
            outputStream.write(buffer, 0, length);
        }

        outputStream.flush();
        outputStream.close();
        inputStream.close();
    }

//    void addDefect(String title,
//                   byte[] image, String description, String causes, String solutions){
//        SQLiteDatabase db = this.getWritableDatabase();
//        ContentValues cv = new ContentValues();
//
//        cv.put(COLUMN_TITLE, title);
//        cv.put(COLUMN_IMAGE, image);
//        cv.put(COLUMN_DESCRIPTION, description);
//        cv.put(COLUMN_CAUSES, causes);
//        cv.put(COLUMN_SOLUTIONS, solutions);
//        long result = db.insert(TABLE_NAME,null, cv);
//        if(result == -1){
//            Toast.makeText(context, "Failed", Toast.LENGTH_SHORT).show();
//        }else {
//            Toast.makeText(context, "Added Successfully!", Toast.LENGTH_SHORT).show();
//        }
//    }

    Cursor readAllData(){
        String query = "SELECT * FROM " + TABLE_NAME;
        SQLiteDatabase db = this.getReadableDatabase();

        Cursor cursor = null;
        if(db != null){
            cursor = db.rawQuery(query, null);
        }
        return cursor;
    }

//    void updateData(String title, byte[] image, String description, String causes, String solutions){
//        SQLiteDatabase db = this.getWritableDatabase();
//        ContentValues cv = new ContentValues();
//        cv.put(COLUMN_TITLE, title);
//        cv.put(COLUMN_IMAGE, image);
//        cv.put(COLUMN_DESCRIPTION, description);
//        cv.put(COLUMN_CAUSES, causes);
//        cv.put(COLUMN_SOLUTIONS, solutions);
//
//        long result = db.update(TABLE_NAME, cv, "_id=?", new String[]{title});
//        if(result == -1){
//            Toast.makeText(context, "Failed", Toast.LENGTH_SHORT).show();
//        }else {
//            Toast.makeText(context, "Updated Successfully!", Toast.LENGTH_SHORT).show();
//        }
//
//    }

//    void deleteOneRow(String title){
//        SQLiteDatabase db = this.getWritableDatabase();
//        long result = db.delete(TABLE_NAME, "_id=?", new String[]{title});
//        if(result == -1){
//            Toast.makeText(context, "Failed to Delete.", Toast.LENGTH_SHORT).show();
//        }else{
//            Toast.makeText(context, "Successfully Deleted.", Toast.LENGTH_SHORT).show();
//        }
//    }

//    void deleteAllData(){
//        SQLiteDatabase db = this.getWritableDatabase();
//        db.execSQL("DELETE FROM " + TABLE_NAME);
//    }

}