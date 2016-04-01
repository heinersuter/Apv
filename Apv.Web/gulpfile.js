var gulp = require('gulp');
var browserSync = require('browser-sync');
var typescript = require('gulp-typescript');
var less = require('gulp-less');
//var sourcemaps = require('gulp-sourcemaps');

gulp.task('ts', function() {
    return gulp.src('src/**/*.ts', { base: '.' })
        //.pipe(sourcemaps.init())
        .pipe(typescript({
            target: 'es5',
            module: 'commonjs',
            noEmitOnError: true,
            noImplicitAny: true,
            emitDecoratorMetadata: true,
            experimentalDecorators: true
        }))
        .on('error', function() { process.exit(1); })
        //.pipe(sourcemaps.write('.'))
        .pipe(gulp.dest('.'));
});

gulp.task('less', function() {
    return gulp.src('src/**/*.less', { base: '.' })
        //.pipe(sourcemaps.init())
        .pipe(less({
            paths: ['./']
        }))
        .on('error', function() { process.exit(1); })
        //.pipe(sourcemaps.write('.'))
        .pipe(gulp.dest('.'));
});

gulp.task('serve', function(done) {
    browserSync({
        open: false,
        port: 9000,
        server: {
            baseDir: ['.'],
            middleware: function(req, res, next) {
                res.setHeader('Access-Control-Allow-Origin', '*');
                next();
            }
        }
    }, done);
});

gulp.task('watch-ts', function() {
    gulp.watch('src/**/*.ts', ["ts"]);
});

gulp.task('watch-less', function() {
    gulp.watch('src/**/*.less', ["less"]);
});

gulp.task('watch', ['watch-less', 'watch-ts']);
